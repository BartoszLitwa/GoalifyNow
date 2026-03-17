namespace GoalifyNow.Api.Data;

public class BodyMeasurement
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public double? Waist { get; set; }
    public double? Chest { get; set; }
    public double? Hips { get; set; }
    public double? BicepsL { get; set; }
    public double? BicepsR { get; set; }
    public double? ThighL { get; set; }
    public double? ThighR { get; set; }
    public double? BodyFatPct { get; set; }
}
