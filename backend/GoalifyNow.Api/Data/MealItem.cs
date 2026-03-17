namespace GoalifyNow.Api.Data;

public class MealItem
{
    public Guid Id { get; set; }
    public Guid MealId { get; set; }
    public Guid FoodItemId { get; set; }
    public double ServingSize { get; set; }
    public string ServingUnit { get; set; } = "g";
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }
}
