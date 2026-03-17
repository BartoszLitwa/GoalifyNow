using GoalifyNow.Api.Options;

using Microsoft.Extensions.Options;

using Stripe;
using Stripe.Checkout;

namespace GoalifyNow.Api.Services;

public class StripeBillingService(IOptions<StripeOptions> stripeOptions)
{
    private readonly StripeOptions _stripe = stripeOptions.Value;

    public Session CreateCheckoutSession(long amountCents, string currency, string userId, string tenantId)
    {
        if (!_stripe.Enabled)
        {
            throw new InvalidOperationException("Stripe is disabled");
        }

        StripeConfiguration.ApiKey = _stripe.ApiKey;

        var options = new SessionCreateOptions
        {
            Mode = "payment",
            SuccessUrl = _stripe.SuccessUrl,
            CancelUrl = _stripe.CancelUrl,
            LineItems =
            [
                new SessionLineItemOptions
                {
                    Quantity = 1,
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = currency,
                        UnitAmount = amountCents,
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Starter subscription"
                        }
                    }
                }
            ],
            Metadata = new Dictionary<string, string>
            {
                ["user_id"] = userId,
                ["tenant_id"] = tenantId
            }
        };

        var service = new SessionService();
        return service.Create(options);
    }

    public Event ParseWebhook(string payload, string signature)
    {
        if (!_stripe.Enabled)
        {
            throw new InvalidOperationException("Stripe is disabled");
        }

        return EventUtility.ConstructEvent(payload, signature, _stripe.WebhookSecret);
    }
}
