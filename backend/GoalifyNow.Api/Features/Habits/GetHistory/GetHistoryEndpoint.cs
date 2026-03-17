using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Habits.GetHistory;

public class GetHistoryEndpoint(GoalifyDbContext db) : Endpoint<GetHistoryRequest, List<HabitEntryDto>>
{
    public override void Configure()
    {
        Get("/api/habits/{HabitId}/history");
    }

    public override async Task HandleAsync(GetHistoryRequest req, CancellationToken ct)
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

        var entries = await db.HabitEntries
            .Where(e => e.HabitId == habit.Id)
            .OrderByDescending(e => e.Date)
            .Take(90)
            .Select(e => new HabitEntryDto(e.Date, e.Completed))
            .ToListAsync(ct);

        await Send.OkAsync(entries, ct);
    }
}

public sealed record GetHistoryRequest(Guid HabitId);
public sealed record HabitEntryDto(DateOnly Date, bool Completed);
