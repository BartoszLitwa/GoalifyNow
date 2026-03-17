namespace GoalifyNow.Api.Data;

public enum SetType { Warmup, Working, Failure }

public class WorkoutSet
{
    public Guid Id { get; set; }
    public Guid WorkoutExerciseId { get; set; }
    public int SetNumber { get; set; }
    public double? Weight { get; set; }
    public int? Reps { get; set; }
    public int? DurationSeconds { get; set; }
    public SetType SetType { get; set; } = SetType.Working;
    public double? RPE { get; set; }
}
