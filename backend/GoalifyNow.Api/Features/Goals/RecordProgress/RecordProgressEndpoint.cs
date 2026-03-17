using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Goals.RecordProgress;

public class RecordProgressEndpoint(GoalifyDbContext db) : Endpoint<RecordProgressRequest, RecordProgressResponse>
{
    public override void Configure()
    {
        Post("/api/goals/{GoalId}/progress");
    }

    public override async Task HandleAsync(RecordProgressRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var goal = await db.Goals.FirstOrDefaultAsync(g => g.Id == req.GoalId && g.UserId == userId, ct);

        if (goal is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        goal.CurrentValue = req.Value;
        goal.UpdatedAt = DateTime.UtcNow;

        if (goal.CurrentValue >= goal.TargetValue)
            goal.Status = GoalStatus.Completed;

        var milestones = await db.Milestones.Where(m => m.GoalId == goal.Id && !m.IsReached).ToListAsync(ct);
        var reached = new List<string>();
        foreach (var m in milestones.Where(m => goal.CurrentValue >= m.TargetValue))
        {
            m.IsReached = true;
            m.ReachedAt = DateTime.UtcNow;
            reached.Add(m.Name);
        }

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new RecordProgressResponse(goal.CurrentValue, goal.Status.ToString(), reached), ct);
    }
}

public sealed record RecordProgressRequest(Guid GoalId, double Value);
public sealed record RecordProgressResponse(double CurrentValue, string Status, List<string> MilestonesReached);
