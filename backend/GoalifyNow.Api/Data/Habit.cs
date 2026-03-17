namespace GoalifyNow.Api.Data;

public enum HabitFrequency { Daily, Weekly }

public class Habit
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public HabitFrequency Frequency { get; set; } = HabitFrequency.Daily;
    public int CurrentStreak { get; set; }
    public int LongestStreak { get; set; }
    public int GraceDaysRemaining { get; set; } = 1;
    public DateTime CreatedAt { get; set; }
}
