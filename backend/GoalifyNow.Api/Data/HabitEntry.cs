namespace GoalifyNow.Api.Data;

public class HabitEntry
{
    public Guid Id { get; set; }
    public Guid HabitId { get; set; }
    public DateOnly Date { get; set; }
    public bool Completed { get; set; }
}
