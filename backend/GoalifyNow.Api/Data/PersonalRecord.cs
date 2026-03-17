namespace GoalifyNow.Api.Data;

public class PersonalRecord
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ExerciseId { get; set; }
    public double Weight { get; set; }
    public int Reps { get; set; }
    public DateTime AchievedAt { get; set; }
}
