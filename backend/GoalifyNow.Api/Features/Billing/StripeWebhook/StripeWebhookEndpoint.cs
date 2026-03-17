using GoalifyNow.Api.Data;
using GoalifyNow.Api.Services;

using FastEndpoints;

namespace GoalifyNow.Api.Features.Billing.StripeWebhook;

public class StripeWebhookEndpoint(StripeBillingService stripeBillingService, GoalifyDbContext db) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post("/api/billing/webhooks/stripe");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        using var reader = new StreamReader(HttpContext.Request.Body);
        var payload = await reader.ReadToEndAsync(ct);
        var signature = HttpContext.Request.Headers["Stripe-Signature"].ToString();

        try
        {
            var evt = stripeBillingService.ParseWebhook(payload, signature);
            db.AuditEvents.Add(new AuditEvent
            {
                Id = Guid.NewGuid(),
                EventType = $"stripe.{evt.Type}",
                Actor = "stripe",
                Payload = evt.Id,
                CreatedAtUtc = DateTime.UtcNow
            });
            await db.SaveChangesAsync(ct);
            await Send.OkAsync(ct);
        }
        catch
        {
            HttpContext.Response.StatusCode = 400;
            await HttpContext.Response.WriteAsync("Invalid webhook", ct);
        }
    }
}
