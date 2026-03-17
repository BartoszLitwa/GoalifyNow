using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Auth.GetProfile;

public class GetProfileEndpoint(GoalifyDbContext db) : EndpointWithoutRequest<ProfileResponse>
{
    public override void Configure()
    {
        Get("/api/auth/me");
    }

    public override async Task HandleAsync(CancellationToken ct)
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

        var prefs = await db.UserPreferences.FirstOrDefaultAsync(x => x.UserId == userId, ct);

        await Send.OkAsync(new ProfileResponse(
            user.Id,
            user.Email,
            user.DisplayName,
            user.AvatarUrl,
            user.FitnessLevel.ToString(),
            user.DateOfBirth,
            user.OnboardingCompleted,
            user.SubscriptionTier.ToString(),
            prefs?.WeightUnit ?? "kg",
            prefs?.DistanceUnit ?? "km",
            prefs?.DarkMode ?? false,
            prefs?.SelectedGoals ?? "[]",
            prefs?.EnabledModules ?? "[]"
        ), ct);
    }
}

public sealed record ProfileResponse(
    Guid Id,
    string Email,
    string DisplayName,
    string? AvatarUrl,
    string FitnessLevel,
    DateOnly? DateOfBirth,
    bool OnboardingCompleted,
    string SubscriptionTier,
    string WeightUnit,
    string DistanceUnit,
    bool DarkMode,
    string SelectedGoals,
    string EnabledModules
);
