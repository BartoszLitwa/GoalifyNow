namespace GoalifyNow.Api.Data;

public class WorkoutTemplate
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ExercisesJson { get; set; } = "[]";
}
