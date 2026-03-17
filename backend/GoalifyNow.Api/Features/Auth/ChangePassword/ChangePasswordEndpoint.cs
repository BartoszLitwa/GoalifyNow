using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Auth.ChangePassword;

public class ChangePasswordEndpoint(GoalifyDbContext db) : Endpoint<ChangePasswordRequest, EmptyResponse>
{
    public override void Configure()
    {
        Post("/api/auth/change-password");
    }

    public override async Task HandleAsync(ChangePasswordRequest req, CancellationToken ct)
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

        if (!BCrypt.Net.BCrypt.Verify(req.CurrentPassword, user.PasswordHash))
        {
            ThrowError("Current password is incorrect", 400);
            return;
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(ct);
    }
}

public sealed record ChangePasswordRequest(string CurrentPassword, string NewPassword);
