namespace GoalifyNow.Api.Data;

public class WeightEntry
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public double WeightKg { get; set; }
    public double? TrendWeightKg { get; set; }
}
