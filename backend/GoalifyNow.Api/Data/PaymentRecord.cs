namespace GoalifyNow.Api.Data;

public class PaymentRecord
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string StripeSessionId { get; set; } = string.Empty;
    public long AmountCents { get; set; }
    public string Currency { get; set; } = "usd";
    public string Status { get; set; } = "created";
    public DateTime CreatedAtUtc { get; set; }
}
