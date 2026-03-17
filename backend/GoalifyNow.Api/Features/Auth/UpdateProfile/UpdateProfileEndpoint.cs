using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Auth.UpdateProfile;

public class UpdateProfileEndpoint(GoalifyDbContext db) : Endpoint<UpdateProfileRequest, EmptyResponse>
{
    public override void Configure()
    {
        Put("/api/auth/profile");
    }

    public override async Task HandleAsync(UpdateProfileRequest req, CancellationToken ct)
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

        if (!string.IsNullOrWhiteSpace(req.DisplayName)) user.DisplayName = req.DisplayName;
        if (req.DateOfBirth.HasValue) user.DateOfBirth = req.DateOfBirth;
        if (req.AvatarUrl is not null) user.AvatarUrl = req.AvatarUrl;

        var prefs = await db.UserPreferences.FirstOrDefaultAsync(x => x.UserId == userId, ct);
        if (prefs is not null)
        {
            if (!string.IsNullOrWhiteSpace(req.WeightUnit)) prefs.WeightUnit = req.WeightUnit;
            if (!string.IsNullOrWhiteSpace(req.DistanceUnit)) prefs.DistanceUnit = req.DistanceUnit;
            if (req.DarkMode.HasValue) prefs.DarkMode = req.DarkMode.Value;
        }

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(ct);
    }
}

public sealed record UpdateProfileRequest(
    string? DisplayName,
    DateOnly? DateOfBirth,
    string? AvatarUrl,
    string? WeightUnit,
    string? DistanceUnit,
    bool? DarkMode
);
