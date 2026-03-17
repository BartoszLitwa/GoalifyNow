namespace GoalifyNow.Api.Data;

public enum GoalCategory { Fitness, Running, Strength, Weight, Nutrition, Habits, Custom }
public enum GoalStatus { Active, Completed, Archived }

public class Goal
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public GoalCategory Category { get; set; }
    public string MetricUnit { get; set; } = string.Empty;
    public double TargetValue { get; set; }
    public double CurrentValue { get; set; }
    public DateOnly? Deadline { get; set; }
    public GoalStatus Status { get; set; } = GoalStatus.Active;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
