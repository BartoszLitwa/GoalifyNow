using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Meals.GetMeals;

public class GetMealsEndpoint(GoalifyDbContext db) : Endpoint<GetMealsRequest, List<MealDto>>
{
    public override void Configure()
    {
        Get("/api/meals");
    }

    public override async Task HandleAsync(GetMealsRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var date = req.Date ?? DateOnly.FromDateTime(DateTime.UtcNow);

        var meals = await db.Meals.Where(m => m.UserId == userId && m.Date == date).OrderBy(m => m.CreatedAt).ToListAsync(ct);
        var mealIds = meals.Select(m => m.Id).ToList();
        var items = await db.MealItems.Where(mi => mealIds.Contains(mi.MealId)).ToListAsync(ct);
        var foodIds = items.Select(i => i.FoodItemId).Distinct().ToList();
        var foods = await db.FoodItems.Where(f => foodIds.Contains(f.Id)).ToDictionaryAsync(f => f.Id, f => f.Name, ct);

        var result = meals.Select(m => {
            var mealItems = items.Where(i => i.MealId == m.Id).Select(i => new MealItemDto(i.Id, i.FoodItemId, foods.GetValueOrDefault(i.FoodItemId, ""), i.ServingSize, i.Calories, i.Protein, i.Carbs, i.Fat)).ToList();
            return new MealDto(m.Id, m.MealType.ToString(), m.Date, mealItems.Sum(i => i.Calories), mealItems.Sum(i => i.Protein), mealItems.Sum(i => i.Carbs), mealItems.Sum(i => i.Fat), mealItems);
        }).ToList();

        await Send.OkAsync(result, ct);
    }
}

public sealed record GetMealsRequest(DateOnly? Date);
public sealed record MealDto(Guid Id, string MealType, DateOnly Date, double TotalCalories, double TotalProtein, double TotalCarbs, double TotalFat, List<MealItemDto> Items);
public sealed record MealItemDto(Guid Id, Guid FoodItemId, string FoodName, double ServingSize, double Calories, double Protein, double Carbs, double Fat);
