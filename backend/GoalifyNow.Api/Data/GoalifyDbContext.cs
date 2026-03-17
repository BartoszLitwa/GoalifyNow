using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Data;

public class GoalifyDbContext(DbContextOptions<GoalifyDbContext> options) : DbContext(options)
{
    public DbSet<UserAccount> Users => Set<UserAccount>();
    public DbSet<UserPreference> UserPreferences => Set<UserPreference>();
    public DbSet<PaymentRecord> Payments => Set<PaymentRecord>();
    public DbSet<AuditEvent> AuditEvents => Set<AuditEvent>();
    public DbSet<Goal> Goals => Set<Goal>();
    public DbSet<Milestone> Milestones => Set<Milestone>();
    public DbSet<Habit> Habits => Set<Habit>();
    public DbSet<HabitEntry> HabitEntries => Set<HabitEntry>();
    public DbSet<WorkoutSession> WorkoutSessions => Set<WorkoutSession>();
    public DbSet<WorkoutExercise> WorkoutExercises => Set<WorkoutExercise>();
    public DbSet<WorkoutSet> WorkoutSets => Set<WorkoutSet>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<WorkoutTemplate> WorkoutTemplates => Set<WorkoutTemplate>();
    public DbSet<PersonalRecord> PersonalRecords => Set<PersonalRecord>();
    public DbSet<Meal> Meals => Set<Meal>();
    public DbSet<MealItem> MealItems => Set<MealItem>();
    public DbSet<FoodItem> FoodItems => Set<FoodItem>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<MacroTarget> MacroTargets => Set<MacroTarget>();
    public DbSet<WaterEntry> WaterEntries => Set<WaterEntry>();
    public DbSet<WeightEntry> WeightEntries => Set<WeightEntry>();
    public DbSet<BodyMeasurement> BodyMeasurements => Set<BodyMeasurement>();
    public DbSet<ProgressPhoto> ProgressPhotos => Set<ProgressPhoto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.Email).HasMaxLength(256);
            entity.Property(x => x.Role).HasMaxLength(64);
            entity.Property(x => x.PasswordHash).HasMaxLength(256);
            entity.Property(x => x.DisplayName).HasMaxLength(256);
            entity.Property(x => x.AvatarUrl).HasMaxLength(1024);
        });

        modelBuilder.Entity<UserPreference>(entity =>
        {
            entity.HasIndex(x => x.UserId).IsUnique();
            entity.Property(x => x.WeightUnit).HasMaxLength(8);
            entity.Property(x => x.DistanceUnit).HasMaxLength(8);
            entity.Property(x => x.EnabledModules).HasMaxLength(1024);
            entity.Property(x => x.SelectedGoals).HasMaxLength(1024);
        });

        modelBuilder.Entity<PaymentRecord>(entity =>
        {
            entity.Property(x => x.StripeSessionId).HasMaxLength(128);
            entity.Property(x => x.Currency).HasMaxLength(8);
            entity.Property(x => x.Status).HasMaxLength(64);
        });

        modelBuilder.Entity<AuditEvent>(entity =>
        {
            entity.Property(x => x.EventType).HasMaxLength(128);
            entity.Property(x => x.Actor).HasMaxLength(256);
            entity.Property(x => x.Payload).HasMaxLength(4000);
        });

        modelBuilder.Entity<Goal>(entity =>
        {
            entity.HasIndex(x => x.UserId);
            entity.Property(x => x.Name).HasMaxLength(256);
            entity.Property(x => x.MetricUnit).HasMaxLength(32);
        });

        modelBuilder.Entity<Milestone>(entity =>
        {
            entity.HasIndex(x => x.GoalId);
            entity.Property(x => x.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<Habit>(entity =>
        {
            entity.HasIndex(x => x.UserId);
            entity.Property(x => x.Name).HasMaxLength(256);
        });

        modelBuilder.Entity<HabitEntry>(entity =>
        {
            entity.HasIndex(x => new { x.HabitId, x.Date }).IsUnique();
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(256);
            entity.Property(x => x.PrimaryMuscle).HasMaxLength(64);
            entity.Property(x => x.SecondaryMuscles).HasMaxLength(256);
            entity.Property(x => x.Equipment).HasMaxLength(64);
        });

        modelBuilder.Entity<WorkoutSession>(entity =>
        {
            entity.HasIndex(x => x.UserId);
            entity.Property(x => x.Name).HasMaxLength(256);
            entity.Property(x => x.Notes).HasMaxLength(2000);
        });

        modelBuilder.Entity<WorkoutTemplate>(entity =>
        {
            entity.HasIndex(x => x.UserId);
            entity.Property(x => x.Name).HasMaxLength(256);
            entity.Property(x => x.ExercisesJson).HasMaxLength(8000);
        });

        modelBuilder.Entity<PersonalRecord>(entity =>
        {
            entity.HasIndex(x => new { x.UserId, x.ExerciseId });
        });

        modelBuilder.Entity<FoodItem>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(256);
            entity.Property(x => x.Brand).HasMaxLength(256);
            entity.Property(x => x.Barcode).HasMaxLength(64);
            entity.HasIndex(x => x.Barcode);
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasIndex(x => new { x.UserId, x.Date });
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasIndex(x => x.UserId);
            entity.Property(x => x.Name).HasMaxLength(256);
            entity.Property(x => x.IngredientsJson).HasMaxLength(8000);
        });

        modelBuilder.Entity<MacroTarget>(entity =>
        {
            entity.HasIndex(x => x.UserId).IsUnique();
        });

        modelBuilder.Entity<WeightEntry>(entity =>
        {
            entity.HasIndex(x => new { x.UserId, x.Date });
        });

        modelBuilder.Entity<BodyMeasurement>(entity =>
        {
            entity.HasIndex(x => new { x.UserId, x.Date });
        });

        modelBuilder.Entity<ProgressPhoto>(entity =>
        {
            entity.HasIndex(x => x.UserId);
            entity.Property(x => x.PoseType).HasMaxLength(32);
            entity.Property(x => x.StoragePath).HasMaxLength(1024);
            entity.Property(x => x.Notes).HasMaxLength(500);
        });
    }
}
