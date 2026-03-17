using System.Text;

using GoalifyNow.Api.Data;
using GoalifyNow.Api.Options;
using GoalifyNow.Api.Services;

using FastEndpoints;
using FastEndpoints.Swagger;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Stripe;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.Configure<StripeOptions>(builder.Configuration.GetSection(StripeOptions.SectionName));
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.SectionName));
builder.Services.Configure<CorsOptions>(builder.Configuration.GetSection(CorsOptions.SectionName));

var dbOptions = builder.Configuration.GetSection(DatabaseOptions.SectionName).Get<DatabaseOptions>() ?? new DatabaseOptions();
var connectionString = builder.Configuration.GetConnectionString("GoalifyDb")
    ?? "Data Source=goalify.db";

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<GoalifyDbContext>(opt =>
        opt.UseSqlite(connectionString, sql => sql.CommandTimeout(dbOptions.CommandTimeoutSeconds))
           .EnableDetailedErrors(dbOptions.EnableDetailedErrors)
           .EnableSensitiveDataLogging(dbOptions.EnableSensitiveDataLogging));
}
else
{
    builder.Services.AddDbContext<GoalifyDbContext>(opt =>
        opt.UseSqlServer(connectionString, sql => sql.CommandTimeout(dbOptions.CommandTimeoutSeconds))
           .EnableDetailedErrors(dbOptions.EnableDetailedErrors)
           .EnableSensitiveDataLogging(dbOptions.EnableSensitiveDataLogging));
}

var jwt = builder.Configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ?? new JwtOptions();
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = true,
            ValidIssuer = jwt.Issuer,
            ValidateAudience = true,
            ValidAudience = jwt.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<StripeBillingService>();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();

var corsConfig = builder.Configuration.GetSection(CorsOptions.SectionName).Get<CorsOptions>() ?? new CorsOptions();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        if (corsConfig.AllowedOrigins.Length > 0)
        {
            policy.WithOrigins(corsConfig.AllowedOrigins)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        }
        else
        {
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    });
});

builder.Services.AddHealthChecks()
    .AddDbContextCheck<GoalifyDbContext>("database");

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<GoalifyDbContext>();

    if (dbOptions.RunMigrations)
    {
        await db.Database.MigrateAsync();
    }

    if (dbOptions.SeedData && !await db.Users.AnyAsync())
    {
        await SeedDataAsync(db);
    }
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseSwaggerGen();
app.MapHealthChecks("/health");

app.Run();

static async Task SeedDataAsync(GoalifyDbContext db)
{
    var demoUser = UserAccount.Create("demo@goalifynow.local", "Demo123!", "User", "Demo User");
    demoUser.Id = DemoData.DemoUserId;
    demoUser.OnboardingCompleted = true;
    demoUser.FitnessLevel = FitnessLevel.Intermediate;
    db.Users.Add(demoUser);

    db.UserPreferences.Add(new UserPreference
    {
        Id = DemoData.GenerateId("preference-demo"),
        UserId = demoUser.Id,
        WeightUnit = "kg",
        DistanceUnit = "km",
        SelectedGoals = "[\"lose-weight\",\"build-muscle\",\"build-habits\"]",
        EnabledModules = "[\"goals\",\"habits\",\"workouts\",\"nutrition\",\"progress\"]",
        DarkMode = false
    });

    db.AuditEvents.Add(new AuditEvent
    {
        EventType = "seed.initialized",
        Actor = "system",
        Payload = "GoalifyNow seed initialized",
        CreatedAtUtc = DateTime.UtcNow
    });

    SeedExercises(db);
    SeedFoodItems(db);
    SeedGoals(db, demoUser.Id);
    SeedHabits(db, demoUser.Id);
    SeedWorkoutSessions(db, demoUser.Id);
    SeedNutrition(db, demoUser.Id);
    SeedProgress(db, demoUser.Id);

    await db.SaveChangesAsync();
}

static void SeedGoals(GoalifyDbContext db, Guid userId)
{
    var goal1Id = DemoData.GenerateId("goal-lose10kg");
    db.Goals.Add(new Goal { Id = goal1Id, UserId = userId, Name = "Lose 10kg by June", Category = GoalCategory.Weight, MetricUnit = "kg", TargetValue = 10, CurrentValue = 6.2, Deadline = new DateOnly(2026, 6, 30), CreatedAt = DateTime.UtcNow.AddDays(-60), UpdatedAt = DateTime.UtcNow });
    db.Milestones.Add(new Milestone { Id = DemoData.GenerateId("milestone-3kg"), GoalId = goal1Id, Name = "3kg lost", TargetValue = 3, IsReached = true, ReachedAt = DateTime.UtcNow.AddDays(-30) });
    db.Milestones.Add(new Milestone { Id = DemoData.GenerateId("milestone-5kg"), GoalId = goal1Id, Name = "5kg lost", TargetValue = 5, IsReached = true, ReachedAt = DateTime.UtcNow.AddDays(-14) });
    db.Milestones.Add(new Milestone { Id = DemoData.GenerateId("milestone-7kg"), GoalId = goal1Id, Name = "7kg lost", TargetValue = 7, IsReached = false });
    db.Milestones.Add(new Milestone { Id = DemoData.GenerateId("milestone-10kg"), GoalId = goal1Id, Name = "10kg lost", TargetValue = 10, IsReached = false });

    var goal2Id = DemoData.GenerateId("goal-run100km");
    db.Goals.Add(new Goal { Id = goal2Id, UserId = userId, Name = "Run 100km this month", Category = GoalCategory.Running, MetricUnit = "km", TargetValue = 100, CurrentValue = 45, Deadline = new DateOnly(2026, 3, 31), CreatedAt = DateTime.UtcNow.AddDays(-20), UpdatedAt = DateTime.UtcNow });
    db.Milestones.Add(new Milestone { Id = DemoData.GenerateId("milestone-25km"), GoalId = goal2Id, Name = "25km", TargetValue = 25, IsReached = true, ReachedAt = DateTime.UtcNow.AddDays(-10) });
    db.Milestones.Add(new Milestone { Id = DemoData.GenerateId("milestone-50km"), GoalId = goal2Id, Name = "50km", TargetValue = 50, IsReached = false });

    var goal3Id = DemoData.GenerateId("goal-bench100kg");
    db.Goals.Add(new Goal { Id = goal3Id, UserId = userId, Name = "Hit 100kg bench press", Category = GoalCategory.Strength, MetricUnit = "kg", TargetValue = 100, CurrentValue = 85, Deadline = new DateOnly(2026, 4, 15), CreatedAt = DateTime.UtcNow.AddDays(-45), UpdatedAt = DateTime.UtcNow });
    db.Milestones.Add(new Milestone { Id = DemoData.GenerateId("milestone-70kg-bp"), GoalId = goal3Id, Name = "70kg", TargetValue = 70, IsReached = true, ReachedAt = DateTime.UtcNow.AddDays(-45) });
    db.Milestones.Add(new Milestone { Id = DemoData.GenerateId("milestone-85kg-bp"), GoalId = goal3Id, Name = "85kg", TargetValue = 85, IsReached = true, ReachedAt = DateTime.UtcNow.AddDays(-7) });
    db.Milestones.Add(new Milestone { Id = DemoData.GenerateId("milestone-100kg-bp"), GoalId = goal3Id, Name = "100kg", TargetValue = 100, IsReached = false });
}

static void SeedHabits(GoalifyDbContext db, Guid userId)
{
    var habits = new[]
    {
        ("Morning workout", "habit-morning-workout", 12, 47),
        ("Log all meals", "habit-log-meals", 8, 15),
        ("Drink 2L water", "habit-drink-water", 5, 21),
        ("Meditate 10 min", "habit-meditate", 0, 8),
        ("Read 20 pages", "habit-read", 23, 23),
    };

    foreach (var (name, idSeed, streak, longest) in habits)
    {
        var h = new Habit
        {
            Id = DemoData.GenerateId(idSeed),
            UserId = userId,
            Name = name,
            Frequency = HabitFrequency.Daily,
            CurrentStreak = streak,
            LongestStreak = longest,
            CreatedAt = DateTime.UtcNow.AddDays(-60)
        };
        db.Habits.Add(h);
        for (var i = 0; i < 14; i++)
        {
            db.HabitEntries.Add(new HabitEntry
            {
                Id = DemoData.GenerateId($"{idSeed}-entry-{i}"),
                HabitId = h.Id,
                Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-i)),
                Completed = i < streak
            });
        }
    }
}

static void SeedExercises(GoalifyDbContext db)
{
    var exercises = new (string Name, string Muscle, string Equipment)[]
    {
        ("Barbell Bench Press", "Chest", "Barbell"), ("Incline Dumbbell Press", "Chest", "Dumbbell"),
        ("Cable Fly", "Chest", "Cable"), ("Push-Up", "Chest", "Bodyweight"),
        ("Barbell Squat", "Quads", "Barbell"), ("Leg Press", "Quads", "Machine"),
        ("Leg Extension", "Quads", "Machine"), ("Bulgarian Split Squat", "Quads", "Dumbbell"),
        ("Deadlift", "Back", "Barbell"), ("Barbell Row", "Back", "Barbell"),
        ("Lat Pulldown", "Back", "Cable"), ("Seated Cable Row", "Back", "Cable"),
        ("Pull-Up", "Back", "Bodyweight"), ("Chin-Up", "Biceps", "Bodyweight"),
        ("Overhead Press", "Shoulders", "Barbell"), ("Lateral Raise", "Shoulders", "Dumbbell"),
        ("Face Pull", "Shoulders", "Cable"), ("Barbell Curl", "Biceps", "Barbell"),
        ("Dumbbell Curl", "Biceps", "Dumbbell"), ("Hammer Curl", "Biceps", "Dumbbell"),
        ("Tricep Pushdown", "Triceps", "Cable"), ("Overhead Tricep Extension", "Triceps", "Dumbbell"),
        ("Dip", "Triceps", "Bodyweight"), ("Romanian Deadlift", "Hamstrings", "Barbell"),
        ("Leg Curl", "Hamstrings", "Machine"), ("Calf Raise", "Calves", "Machine"),
        ("Plank", "Core", "Bodyweight"), ("Cable Crunch", "Core", "Cable"),
        ("Hanging Leg Raise", "Core", "Bodyweight"), ("Hip Thrust", "Glutes", "Barbell"),
    };

    foreach (var (name, muscle, equipment) in exercises)
    {
        db.Exercises.Add(new Exercise
        {
            Id = DemoData.GenerateId($"exercise-{name.ToLowerInvariant().Replace(' ', '-')}"),
            Name = name,
            PrimaryMuscle = muscle,
            Equipment = equipment
        });
    }
}

static void SeedFoodItems(GoalifyDbContext db)
{
    var foods = new (string Name, double Cal, double P, double C, double F)[]
    {
        ("Chicken Breast", 165, 31, 0, 3.6), ("Salmon Fillet", 208, 20, 0, 13),
        ("Brown Rice", 112, 2.6, 23, 0.9), ("White Rice", 130, 2.7, 28, 0.3),
        ("Oatmeal", 68, 2.4, 12, 1.4), ("Whole Wheat Bread", 247, 13, 41, 3.4),
        ("Greek Yogurt", 59, 10, 3.6, 0.4), ("Banana", 89, 1.1, 23, 0.3),
        ("Apple", 52, 0.3, 14, 0.2), ("Egg (Large)", 155, 13, 1.1, 11),
        ("Sweet Potato", 86, 1.6, 20, 0.1), ("Broccoli", 34, 2.8, 7, 0.4),
        ("Spinach", 23, 2.9, 3.6, 0.4), ("Avocado", 160, 2, 9, 15),
        ("Almonds", 579, 21, 22, 50), ("Peanut Butter", 588, 25, 20, 50),
        ("Olive Oil", 884, 0, 0, 100), ("Tofu", 76, 8, 1.9, 4.8),
        ("Cottage Cheese", 98, 11, 3.4, 4.3), ("Milk (Whole)", 61, 3.2, 4.8, 3.3),
        ("Turkey Breast", 135, 30, 0, 0.7), ("Tuna (Canned)", 116, 26, 0, 0.8),
        ("Quinoa", 120, 4.4, 21, 1.9), ("Lentils", 116, 9, 20, 0.4),
        ("Blueberries", 57, 0.7, 14, 0.3), ("Protein Powder (Whey)", 400, 80, 8, 5),
    };

    foreach (var (name, cal, p, c, f) in foods)
    {
        db.FoodItems.Add(new FoodItem
        {
            Id = DemoData.GenerateId($"food-{name.ToLowerInvariant().Replace(' ', '-')}"),
            Name = name,
            CaloriesPer100g = cal,
            ProteinPer100g = p,
            CarbsPer100g = c,
            FatPer100g = f,
            IsVerified = true
        });
    }
}

static void SeedWorkoutSessions(GoalifyDbContext db, Guid userId)
{
    var benchId = DemoData.GenerateId("exercise-barbell-bench-press");
    var squatId = DemoData.GenerateId("exercise-barbell-squat");
    var deadliftId = DemoData.GenerateId("exercise-deadlift");
    var pullUpId = DemoData.GenerateId("exercise-pull-up");

    for (var i = 0; i < 8; i++)
    {
        var sessionId = DemoData.GenerateId($"session-{i}");
        var startedAt = DateTime.UtcNow.AddDays(-i * 2).AddHours(-1);
        var session = new WorkoutSession
        {
            Id = sessionId,
            UserId = userId,
            Name = i % 2 == 0 ? "Push Day" : "Pull Day",
            StartedAt = startedAt,
            CompletedAt = startedAt.AddMinutes(55 + (i * 3)),
            Notes = $"Session #{i + 1}"
        };
        db.WorkoutSessions.Add(session);

        var exerciseId = i % 2 == 0 ? benchId : deadliftId;
        var we = new WorkoutExercise
        {
            Id = DemoData.GenerateId($"we-{i}-0"),
            SessionId = sessionId,
            ExerciseId = exerciseId,
            Order = 0
        };
        db.WorkoutExercises.Add(we);

        for (var s = 0; s < 4; s++)
        {
            db.WorkoutSets.Add(new WorkoutSet
            {
                Id = DemoData.GenerateId($"set-{i}-0-{s}"),
                WorkoutExerciseId = we.Id,
                SetNumber = s + 1,
                Reps = 8 + (s % 3),
                Weight = 60 + (i * 2.5) - (s * 2.5)
            });
        }

        var exerciseId2 = i % 2 == 0 ? squatId : pullUpId;
        var we2 = new WorkoutExercise
        {
            Id = DemoData.GenerateId($"we-{i}-1"),
            SessionId = sessionId,
            ExerciseId = exerciseId2,
            Order = 1
        };
        db.WorkoutExercises.Add(we2);

        for (var s = 0; s < 3; s++)
        {
            db.WorkoutSets.Add(new WorkoutSet
            {
                Id = DemoData.GenerateId($"set-{i}-1-{s}"),
                WorkoutExerciseId = we2.Id,
                SetNumber = s + 1,
                Reps = 10 + s,
                Weight = i % 2 == 0 ? 80 + (i * 2.5) : 0
            });
        }
    }
}

static void SeedNutrition(GoalifyDbContext db, Guid userId)
{
    db.MacroTargets.Add(new MacroTarget
    {
        Id = DemoData.GenerateId("macro-target-demo"),
        UserId = userId,
        Calories = 2200,
        Protein = 180,
        Carbs = 220,
        Fat = 73
    });

    var chickenId = DemoData.GenerateId("food-chicken-breast");
    var riceId = DemoData.GenerateId("food-brown-rice");
    var oatmealId = DemoData.GenerateId("food-oatmeal");
    var eggId = DemoData.GenerateId("food-egg-(large)");
    var bananaId = DemoData.GenerateId("food-banana");
    var yogurtId = DemoData.GenerateId("food-greek-yogurt");

    for (var day = 0; day < 5; day++)
    {
        var date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-day));

        var breakfast = new Meal
        {
            Id = DemoData.GenerateId($"meal-breakfast-{day}"),
            UserId = userId,
            MealType = MealType.Breakfast,
            Date = date,
            CreatedAt = DateTime.UtcNow.AddDays(-day).AddHours(-10)
        };
        db.Meals.Add(breakfast);
        db.MealItems.Add(new MealItem { Id = DemoData.GenerateId($"mi-bfast-oat-{day}"), MealId = breakfast.Id, FoodItemId = oatmealId, ServingSize = 200, Calories = 136, Protein = 4.8, Carbs = 24, Fat = 2.8 });
        db.MealItems.Add(new MealItem { Id = DemoData.GenerateId($"mi-bfast-egg-{day}"), MealId = breakfast.Id, FoodItemId = eggId, ServingSize = 100, Calories = 155, Protein = 13, Carbs = 1.1, Fat = 11 });
        db.MealItems.Add(new MealItem { Id = DemoData.GenerateId($"mi-bfast-ban-{day}"), MealId = breakfast.Id, FoodItemId = bananaId, ServingSize = 120, Calories = 107, Protein = 1.3, Carbs = 27.6, Fat = 0.4 });

        var lunch = new Meal
        {
            Id = DemoData.GenerateId($"meal-lunch-{day}"),
            UserId = userId,
            MealType = MealType.Lunch,
            Date = date,
            CreatedAt = DateTime.UtcNow.AddDays(-day).AddHours(-6)
        };
        db.Meals.Add(lunch);
        db.MealItems.Add(new MealItem { Id = DemoData.GenerateId($"mi-lunch-chk-{day}"), MealId = lunch.Id, FoodItemId = chickenId, ServingSize = 200, Calories = 330, Protein = 62, Carbs = 0, Fat = 7.2 });
        db.MealItems.Add(new MealItem { Id = DemoData.GenerateId($"mi-lunch-rice-{day}"), MealId = lunch.Id, FoodItemId = riceId, ServingSize = 250, Calories = 280, Protein = 6.5, Carbs = 57.5, Fat = 2.25 });

        db.WaterEntries.Add(new WaterEntry
        {
            Id = DemoData.GenerateId($"water-{day}"),
            UserId = userId,
            AmountMl = 1800 + (day * 200),
            Date = date
        });
    }
}

static void SeedProgress(GoalifyDbContext db, Guid userId)
{
    for (var i = 30; i >= 0; i--)
    {
        var weight = 82.5 - (i * 0.2) + (Random.Shared.NextDouble() * 0.6 - 0.3);
        var date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-i));
        var trendWeight = 82.5 - (i * 0.15);

        db.WeightEntries.Add(new WeightEntry
        {
            Id = DemoData.GenerateId($"weight-{i}"),
            UserId = userId,
            Date = date,
            WeightKg = Math.Round(weight, 1),
            TrendWeightKg = Math.Round(trendWeight, 1)
        });
    }

    for (var w = 0; w < 4; w++)
    {
        db.BodyMeasurements.Add(new BodyMeasurement
        {
            Id = DemoData.GenerateId($"measurement-{w}"),
            UserId = userId,
            Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-w * 7)),
            Waist = 86.0 - (w * 0.3),
            Chest = 102.0 + (w * 0.1),
            BicepsL = 35.5 + (w * 0.2),
            BicepsR = 36.0 + (w * 0.2),
            ThighL = 59.0,
            ThighR = 59.5
        });
    }

    db.ProgressPhotos.Add(new ProgressPhoto
    {
        Id = DemoData.GenerateId("photo-front"),
        UserId = userId,
        PoseType = "Front",
        StoragePath = "photos/demo/front.jpg",
        Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-14)),
        Notes = "Week 2 progress"
    });

    db.ProgressPhotos.Add(new ProgressPhoto
    {
        Id = DemoData.GenerateId("photo-side"),
        UserId = userId,
        PoseType = "Side",
        StoragePath = "photos/demo/side.jpg",
        Date = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-7)),
        Notes = "Week 3 progress"
    });
}

public partial class Program;
