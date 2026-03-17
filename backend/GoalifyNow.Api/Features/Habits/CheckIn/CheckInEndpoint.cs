using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Habits.CheckIn;

public class CheckInEndpoint(GoalifyDbContext db) : Endpoint<CheckInRequest, CheckInResponse>
{
    public override void Configure()
    {
        Post("/api/habits/{HabitId}/check-in");
    }

    public override async Task HandleAsync(CheckInRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var habit = await db.Habits.FirstOrDefaultAsync(h => h.Id == req.HabitId && h.UserId == userId, ct);

        if (habit is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var existing = await db.HabitEntries.FirstOrDefaultAsync(e => e.HabitId == habit.Id && e.Date == today, ct);

        if (existing is not null)
        {
            existing.Completed = !existing.Completed;
        }
        else
        {
            db.HabitEntries.Add(new HabitEntry { Id = Guid.NewGuid(), HabitId = habit.Id, Date = today, Completed = true });
        }

        var completed = existing?.Completed ?? true;
        if (completed)
        {
            habit.CurrentStreak++;
            if (habit.CurrentStreak > habit.LongestStreak)
                habit.LongestStreak = habit.CurrentStreak;
        }
        else
        {
            habit.CurrentStreak = Math.Max(0, habit.CurrentStreak - 1);
        }

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new CheckInResponse(habit.CurrentStreak, habit.LongestStreak, completed), ct);
    }
}

public sealed record CheckInRequest(Guid HabitId);
public sealed record CheckInResponse(int CurrentStreak, int LongestStreak, bool Completed);
