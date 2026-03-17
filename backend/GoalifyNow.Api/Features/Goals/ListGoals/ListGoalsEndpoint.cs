using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Goals.ListGoals;

public class ListGoalsEndpoint(GoalifyDbContext db) : Endpoint<ListGoalsRequest, List<GoalDto>>
{
    public override void Configure()
    {
        Get("/api/goals");
    }

    public override async Task HandleAsync(ListGoalsRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var query = db.Goals.Where(g => g.UserId == userId);

        if (!string.IsNullOrEmpty(req.Status) && Enum.TryParse<GoalStatus>(req.Status, true, out var status))
            query = query.Where(g => g.Status == status);

        var goals = await query.OrderByDescending(g => g.CreatedAt).ToListAsync(ct);
        var goalIds = goals.Select(g => g.Id).ToList();
        var milestones = await db.Milestones.Where(m => goalIds.Contains(m.GoalId)).ToListAsync(ct);

        var result = goals.Select(g => new GoalDto(
            g.Id, g.Name, g.Category.ToString(), g.MetricUnit, g.TargetValue, g.CurrentValue,
            g.Deadline, g.Status.ToString(), g.CreatedAt,
            milestones.Where(m => m.GoalId == g.Id).Select(m => new MilestoneDto(m.Id, m.Name, m.TargetValue, m.IsReached, m.ReachedAt)).ToList()
        )).ToList();

        await Send.OkAsync(result, ct);
    }
}

public sealed record ListGoalsRequest(string? Status);
public sealed record GoalDto(Guid Id, string Name, string Category, string MetricUnit, double TargetValue, double CurrentValue, DateOnly? Deadline, string Status, DateTime CreatedAt, List<MilestoneDto> Milestones);
public sealed record MilestoneDto(Guid Id, string Name, double TargetValue, bool IsReached, DateTime? ReachedAt);
