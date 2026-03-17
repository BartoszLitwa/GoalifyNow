using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Goals.GetGoal;

public class GetGoalEndpoint(GoalifyDbContext db) : Endpoint<GetGoalRequest, GoalDetailDto>
{
    public override void Configure()
    {
        Get("/api/goals/{Id}");
    }

    public override async Task HandleAsync(GetGoalRequest req, CancellationToken ct)
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

        var milestones = await db.Milestones.Where(m => m.GoalId == goal.Id).OrderBy(m => m.TargetValue).ToListAsync(ct);

        await Send.OkAsync(new GoalDetailDto(
            goal.Id, goal.Name, goal.Category.ToString(), goal.MetricUnit, goal.TargetValue,
            goal.CurrentValue, goal.Deadline, goal.Status.ToString(), goal.CreatedAt, goal.UpdatedAt,
            milestones.Select(m => new GoalMilestoneDto(m.Id, m.Name, m.TargetValue, m.IsReached, m.ReachedAt)).ToList()
        ), ct);
    }
}

public sealed record GetGoalRequest(Guid Id);
public sealed record GoalDetailDto(Guid Id, string Name, string Category, string MetricUnit, double TargetValue, double CurrentValue, DateOnly? Deadline, string Status, DateTime CreatedAt, DateTime UpdatedAt, List<GoalMilestoneDto> Milestones);
public sealed record GoalMilestoneDto(Guid Id, string Name, double TargetValue, bool IsReached, DateTime? ReachedAt);
