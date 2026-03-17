namespace GoalifyNow.Api.Data;

public class WaterEntry
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public int AmountMl { get; set; }
}
