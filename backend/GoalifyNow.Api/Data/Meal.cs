namespace GoalifyNow.Api.Data;

public enum MealType { Breakfast, Lunch, Dinner, Snack }

public class Meal
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public MealType MealType { get; set; }
    public DateTime CreatedAt { get; set; }
}
