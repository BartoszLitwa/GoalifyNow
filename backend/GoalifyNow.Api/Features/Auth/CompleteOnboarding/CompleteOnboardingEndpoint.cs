using System.Security.Claims;
using System.Text.Json;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Auth.CompleteOnboarding;

public class CompleteOnboardingEndpoint(GoalifyDbContext db) : Endpoint<OnboardingRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("/api/auth/onboarding");
    }

    public override async Task HandleAsync(OnboardingRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim))
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }
        var userId = Guid.Parse(userIdClaim);
        var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId, ct);

        if (user is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        if (Enum.TryParse<FitnessLevel>(req.FitnessLevel, true, out var level))
            user.FitnessLevel = level;

        user.OnboardingCompleted = true;

        var prefs = await db.UserPreferences.FirstOrDefaultAsync(x => x.UserId == userId, ct);
        if (prefs is not null)
        {
            prefs.SelectedGoals = JsonSerializer.Serialize(req.Goals);
            if (!string.IsNullOrWhiteSpace(req.WeightUnit)) prefs.WeightUnit = req.WeightUnit;
            if (!string.IsNullOrWhiteSpace(req.DistanceUnit)) prefs.DistanceUnit = req.DistanceUnit;
        }

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(ct);
    }
}

public sealed record OnboardingRequest(
    string FitnessLevel,
    string[] Goals,
    string? WeightUnit,
    string? DistanceUnit
);
