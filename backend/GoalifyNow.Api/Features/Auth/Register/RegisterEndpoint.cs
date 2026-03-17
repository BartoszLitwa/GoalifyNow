using GoalifyNow.Api.Data;
using GoalifyNow.Api.Services;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Auth.Register;

public class RegisterEndpoint(GoalifyDbContext db, JwtTokenService tokenService) : Endpoint<RegisterRequest, RegisterResponse>
{
    public override void Configure()
    {
        Post("/api/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        var email = req.Email.Trim().ToLowerInvariant();
        var exists = await db.Users.AnyAsync(x => x.Email == email, ct);

        if (exists)
        {
            ThrowError("Email already exists", 400);
            return;
        }

        var user = UserAccount.Create(email, req.Password, "User", req.DisplayName);
        db.Users.Add(user);
        db.UserPreferences.Add(new UserPreference { Id = Guid.NewGuid(), UserId = user.Id });
        db.AuditEvents.Add(new AuditEvent
        {
            Id = Guid.NewGuid(),
            EventType = "auth.user.registered",
            Actor = user.Email,
            Payload = "User",
            CreatedAtUtc = DateTime.UtcNow
        });
        await db.SaveChangesAsync(ct);

        var token = tokenService.CreateToken(user);
        await Send.OkAsync(new RegisterResponse(token, user.Id, user.DisplayName), ct);
    }
}

public sealed record RegisterRequest(string Email, string Password, string DisplayName);
public sealed record RegisterResponse(string AccessToken, Guid UserId, string DisplayName);
