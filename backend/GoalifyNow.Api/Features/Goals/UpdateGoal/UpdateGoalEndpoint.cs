using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Goals.UpdateGoal;

public class UpdateGoalEndpoint(GoalifyDbContext db) : Endpoint<UpdateGoalRequest, EmptyResponse>
{
    public override void Configure()
    {
        Put("/api/goals/{Id}");
    }

    public override async Task HandleAsync(UpdateGoalRequest req, CancellationToken ct)
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

        if (!string.IsNullOrWhiteSpace(req.Name)) goal.Name = req.Name;
        if (req.TargetValue.HasValue) goal.TargetValue = req.TargetValue.Value;
        if (req.Deadline.HasValue) goal.Deadline = req.Deadline;
        goal.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(ct);
    }
}

public sealed record UpdateGoalRequest(Guid Id, string? Name, double? TargetValue, DateOnly? Deadline);
