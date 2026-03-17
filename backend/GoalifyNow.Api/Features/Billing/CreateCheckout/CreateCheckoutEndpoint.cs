using System.Security.Claims;

using GoalifyNow.Api.Data;
using GoalifyNow.Api.Services;

using FastEndpoints;

namespace GoalifyNow.Api.Features.Billing.CreateCheckout;

public class CreateCheckoutEndpoint(StripeBillingService stripeBillingService, GoalifyDbContext db) : Endpoint<CreateCheckoutRequest, CreateCheckoutResponse>
{
    public override void Configure()
    {
        Post("/api/billing/checkout");
    }

    public override async Task HandleAsync(CreateCheckoutRequest req, CancellationToken ct)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        var session = stripeBillingService.CreateCheckoutSession(req.AmountCents, req.Currency, userId, req.TenantId);

        db.Payments.Add(new PaymentRecord
        {
            Id = Guid.NewGuid(),
            UserId = Guid.TryParse(userId, out var uid) ? uid : Guid.Empty,
            AmountCents = req.AmountCents,
            Currency = req.Currency,
            StripeSessionId = session.Id,
            Status = "created",
            CreatedAtUtc = DateTime.UtcNow
        });

        db.AuditEvents.Add(new AuditEvent
        {
            Id = Guid.NewGuid(),
            EventType = "billing.checkout.created",
            Actor = userId,
            Payload = session.Id,
            CreatedAtUtc = DateTime.UtcNow
        });

        await db.SaveChangesAsync(ct);

        await Send.OkAsync(new CreateCheckoutResponse(session.Id, session.Url ?? string.Empty), ct);
    }
}

public sealed record CreateCheckoutRequest(long AmountCents, string Currency, string TenantId);
public sealed record CreateCheckoutResponse(string SessionId, string Url);
