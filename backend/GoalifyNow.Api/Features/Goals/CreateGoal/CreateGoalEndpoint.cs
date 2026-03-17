using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Goals.CreateGoal;

public class CreateGoalEndpoint(GoalifyDbContext db) : Endpoint<CreateGoalRequest, CreateGoalResponse>
{
    public override void Configure()
    {
        Post("/api/goals");
    }

    public override async Task HandleAsync(CreateGoalRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var now = DateTime.UtcNow;

        var goal = new Goal
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = req.Name,
            Category = Enum.TryParse<GoalCategory>(req.Category, true, out var cat) ? cat : GoalCategory.Custom,
            MetricUnit = req.MetricUnit,
            TargetValue = req.TargetValue,
            CurrentValue = 0,
            Deadline = req.Deadline,
            Status = GoalStatus.Active,
            CreatedAt = now,
            UpdatedAt = now
        };

        db.Goals.Add(goal);

        if (req.Milestones is { Length: > 0 })
        {
            foreach (var m in req.Milestones)
            {
                db.Milestones.Add(new Milestone
                {
                    Id = Guid.NewGuid(),
                    GoalId = goal.Id,
                    Name = m.Name,
                    TargetValue = m.TargetValue
                });
            }
        }

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new CreateGoalResponse(goal.Id), ct);
    }
}

public sealed record CreateGoalRequest(string Name, string Category, string MetricUnit, double TargetValue, DateOnly? Deadline, MilestoneInput[]? Milestones);
public sealed record MilestoneInput(string Name, double TargetValue);
public sealed record CreateGoalResponse(Guid Id);
