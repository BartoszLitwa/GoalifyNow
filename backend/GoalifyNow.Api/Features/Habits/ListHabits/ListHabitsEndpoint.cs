using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Habits.ListHabits;

public class ListHabitsEndpoint(GoalifyDbContext db) : EndpointWithoutRequest<List<HabitDto>>
{
    public override void Configure()
    {
        Get("/api/habits");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var habits = await db.Habits.Where(h => h.UserId == userId).OrderBy(h => h.CreatedAt).ToListAsync(ct);
        var habitIds = habits.Select(h => h.Id).ToList();
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var weekAgo = today.AddDays(-6);

        var entries = await db.HabitEntries
            .Where(e => habitIds.Contains(e.HabitId) && e.Date >= weekAgo && e.Date <= today)
            .ToListAsync(ct);

        var result = habits.Select(h =>
        {
            var habitEntries = entries.Where(e => e.HabitId == h.Id).ToList();
            var todayEntry = habitEntries.FirstOrDefault(e => e.Date == today);
            var lastWeek = Enumerable.Range(0, 7)
                .Select(i => weekAgo.AddDays(i))
                .Select(d => habitEntries.Any(e => e.Date == d && e.Completed))
                .ToList();

            var totalDays = (DateTime.UtcNow - h.CreatedAt).Days + 1;
            var completedDays = totalDays > 0 ? (int)((double)h.CurrentStreak / totalDays * 100) : 0;

            return new HabitDto(h.Id, h.Name, h.Frequency.ToString(), h.CurrentStreak, h.LongestStreak, todayEntry?.Completed ?? false, completedDays, lastWeek);
        }).ToList();

        await Send.OkAsync(result, ct);
    }
}

public sealed record HabitDto(Guid Id, string Name, string Frequency, int CurrentStreak, int LongestStreak, bool TodayDone, int CompletionRate, List<bool> LastWeek);
