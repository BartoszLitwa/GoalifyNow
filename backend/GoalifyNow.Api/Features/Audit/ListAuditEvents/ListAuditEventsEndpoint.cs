using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Audit.ListAuditEvents;

public class ListAuditEventsEndpoint(GoalifyDbContext db) : EndpointWithoutRequest<List<AuditEventDto>>
{
    public override void Configure()
    {
        Get("/api/audit/events");
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

        var isAdmin = User.IsInRole("Admin");
        var query = db.AuditEvents.AsQueryable();
        if (!isAdmin)
            query = query.Where(x => x.Actor == user.Email);

        var result = await query
            .OrderByDescending(x => x.CreatedAtUtc)
            .Take(200)
            .Select(x => new AuditEventDto(x.Id, x.EventType, x.Actor, x.Payload, x.CreatedAtUtc))
            .ToListAsync(ct);

        await Send.OkAsync(result, ct);
    }
}

public sealed record AuditEventDto(Guid Id, string EventType, string Actor, string Payload, DateTime CreatedAtUtc);
