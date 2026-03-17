using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Nutrition.GetDailySummary;

public class GetDailySummaryEndpoint(GoalifyDbContext db) : Endpoint<DailySummaryRequest, DailySummaryDto>
{
    public override void Configure()
    {
        Get("/api/nutrition/daily");
    }

    public override async Task HandleAsync(DailySummaryRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var date = req.Date ?? DateOnly.FromDateTime(DateTime.UtcNow);

        var meals = await db.Meals.Where(m => m.UserId == userId && m.Date == date).Select(m => m.Id).ToListAsync(ct);
        var items = await db.MealItems.Where(mi => meals.Contains(mi.MealId)).ToListAsync(ct);
        var water = await db.WaterEntries.Where(w => w.UserId == userId && w.Date == date).SumAsync(w => w.AmountMl, ct);
        var targets = await db.MacroTargets.FirstOrDefaultAsync(t => t.UserId == userId, ct);

        await Send.OkAsync(new DailySummaryDto(
            date,
            items.Sum(i => i.Calories),
            items.Sum(i => i.Protein),
            items.Sum(i => i.Carbs),
            items.Sum(i => i.Fat),
            water,
            targets?.Calories ?? 2200,
            targets?.Protein ?? 150,
            targets?.Carbs ?? 250,
            targets?.Fat ?? 70
        ), ct);
    }
}

public sealed record DailySummaryRequest(DateOnly? Date);
public sealed record DailySummaryDto(DateOnly Date, double Calories, double Protein, double Carbs, double Fat, int WaterMl, double TargetCalories, double TargetProtein, double TargetCarbs, double TargetFat);
