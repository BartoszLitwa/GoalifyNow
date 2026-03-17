using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Meals.LogMeal;

public class LogMealEndpoint(GoalifyDbContext db) : Endpoint<LogMealRequest, LogMealResponse>
{
    public override void Configure()
    {
        Post("/api/meals");
    }

    public override async Task HandleAsync(LogMealRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var meal = new Meal
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Date = req.Date ?? DateOnly.FromDateTime(DateTime.UtcNow),
            MealType = Enum.TryParse<MealType>(req.MealType, true, out var mt) ? mt : MealType.Snack,
            CreatedAt = DateTime.UtcNow
        };
        db.Meals.Add(meal);

        if (req.Items is { Count: > 0 })
        {
            foreach (var item in req.Items)
            {
                db.MealItems.Add(new MealItem
                {
                    Id = Guid.NewGuid(), MealId = meal.Id, FoodItemId = item.FoodItemId,
                    ServingSize = item.ServingSize, ServingUnit = item.ServingUnit ?? "g",
                    Calories = item.Calories, Protein = item.Protein, Carbs = item.Carbs, Fat = item.Fat
                });
            }
        }

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new LogMealResponse(meal.Id), ct);
    }
}

public sealed record LogMealRequest(string MealType, DateOnly? Date, List<MealItemInput>? Items);
public sealed record MealItemInput(Guid FoodItemId, double ServingSize, string? ServingUnit, double Calories, double Protein, double Carbs, double Fat);
public sealed record LogMealResponse(Guid Id);
