namespace GoalifyNow.Api.Data;

public class WorkoutExercise
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public Guid ExerciseId { get; set; }
    public int Order { get; set; }
    public string? Notes { get; set; }
}
