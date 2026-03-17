namespace GoalifyNow.Api.Options;

public class StripeOptions
{
    public const string SectionName = "Stripe";
    public bool Enabled { get; set; } = false;
    public string ApiKey { get; set; } = string.Empty;
    public string PublishableKey { get; set; } = string.Empty;
    public string WebhookSecret { get; set; } = string.Empty;
    public string SuccessUrl { get; set; } = "http://localhost:4200/billing?success=true";
    public string CancelUrl { get; set; } = "http://localhost:4200/billing?canceled=true";
}
