using GoalifyNow.Api.Data;
using GoalifyNow.Api.Services;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Auth.Login;

public class LoginEndpoint(GoalifyDbContext db, JwtTokenService tokenService) : Endpoint<LoginRequest, LoginResponse>
{
    public override void Configure()
    {
        Post("/api/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var email = req.Email.Trim().ToLowerInvariant();
        var user = await db.Users.FirstOrDefaultAsync(x => x.Email == email, ct);

        if (user is null || !BCrypt.Net.BCrypt.Verify(req.Password, user.PasswordHash))
        {
            await HttpContext.Response.SendAsync(new LoginResponse(string.Empty, "Invalid credentials"), 401, null, ct);
            return;
        }

        var token = tokenService.CreateToken(user);
        db.AuditEvents.Add(new AuditEvent
        {
            Id = Guid.NewGuid(),
            EventType = "auth.user.login",
            Actor = user.Email,
            Payload = "success",
            CreatedAtUtc = DateTime.UtcNow
        });
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(new LoginResponse(token, "Logged in"), ct);
    }
}

public sealed record LoginRequest(string Email, string Password);
public sealed record LoginResponse(string AccessToken, string Message);
