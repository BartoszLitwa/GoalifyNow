using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Workouts.StartSession;

public class StartSessionEndpoint(GoalifyDbContext db) : Endpoint<StartSessionRequest, StartSessionResponse>
{
    public override void Configure()
    {
        Post("/api/workouts");
    }

    public override async Task HandleAsync(StartSessionRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var session = new WorkoutSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = req.Name,
            StartedAt = DateTime.UtcNow,
            TemplateId = req.TemplateId
        };
        db.WorkoutSessions.Add(session);
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new StartSessionResponse(session.Id), ct);
    }
}

public sealed record StartSessionRequest(string Name, Guid? TemplateId);
public sealed record StartSessionResponse(Guid Id);
