using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Nutrition.SetTargets;

public class SetTargetsEndpoint(GoalifyDbContext db) : Endpoint<SetTargetsRequest, EmptyResponse>
{
    public override void Configure()
    {
        Put("/api/nutrition/targets");
    }

    public override async Task HandleAsync(SetTargetsRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var existing = await db.MacroTargets.FirstOrDefaultAsync(t => t.UserId == userId, ct);

        if (existing is not null)
        {
            existing.Calories = req.Calories;
            existing.Protein = req.Protein;
            existing.Carbs = req.Carbs;
            existing.Fat = req.Fat;
        }
        else
        {
            db.MacroTargets.Add(new MacroTarget { Id = Guid.NewGuid(), UserId = userId, Calories = req.Calories, Protein = req.Protein, Carbs = req.Carbs, Fat = req.Fat });
        }

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(ct);
    }
}

public sealed record SetTargetsRequest(double Calories, double Protein, double Carbs, double Fat);
