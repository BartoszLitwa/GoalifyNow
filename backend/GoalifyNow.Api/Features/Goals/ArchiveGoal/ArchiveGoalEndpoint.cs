using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Goals.ArchiveGoal;

public class ArchiveGoalEndpoint(GoalifyDbContext db) : Endpoint<ArchiveGoalRequest, EmptyResponse>
{
    public override void Configure()
    {
        Delete("/api/goals/{Id}");
    }

    public override async Task HandleAsync(ArchiveGoalRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var goal = await db.Goals.FirstOrDefaultAsync(g => g.Id == req.Id && g.UserId == userId, ct);

        if (goal is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        goal.Status = GoalStatus.Archived;
        goal.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(ct);
    }
}

public sealed record ArchiveGoalRequest(Guid Id);
