namespace GoalifyNow.Api.Data;

public class Milestone
{
    public Guid Id { get; set; }
    public Guid GoalId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double TargetValue { get; set; }
    public bool IsReached { get; set; }
    public DateTime? ReachedAt { get; set; }
}
