using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Progress.GetDashboard;

public class GetDashboardEndpoint(GoalifyDbContext db) : EndpointWithoutRequest<DashboardDto>
{
    public override void Configure()
    {
        Get("/api/progress/dashboard");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);

        var activeGoals = await db.Goals.CountAsync(g => g.UserId == userId && g.Status == GoalStatus.Active, ct);
        var completedGoals = await db.Goals.CountAsync(g => g.UserId == userId && g.Status == GoalStatus.Completed, ct);

        var weekAgo = DateTime.UtcNow.AddDays(-7);
        var weekWorkouts = await db.WorkoutSessions.CountAsync(s => s.UserId == userId && s.StartedAt >= weekAgo, ct);

        var latestWeight = await db.WeightEntries.Where(w => w.UserId == userId).OrderByDescending(w => w.Date).FirstOrDefaultAsync(ct);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var todayMeals = await db.Meals.Where(m => m.UserId == userId && m.Date == today).Select(m => m.Id).ToListAsync(ct);
        var todayCalories = await db.MealItems.Where(mi => todayMeals.Contains(mi.MealId)).SumAsync(mi => mi.Calories, ct);

        var longestHabitStreak = await db.Habits.Where(h => h.UserId == userId).MaxAsync(h => (int?)h.LongestStreak, ct) ?? 0;

        await Send.OkAsync(new DashboardDto(
            activeGoals, completedGoals, weekWorkouts,
            latestWeight?.WeightKg, latestWeight?.TrendWeightKg,
            todayCalories, longestHabitStreak
        ), ct);
    }
}

public sealed record DashboardDto(int ActiveGoals, int CompletedGoals, int WeekWorkouts, double? CurrentWeight, double? TrendWeight, double TodayCalories, int LongestHabitStreak);
