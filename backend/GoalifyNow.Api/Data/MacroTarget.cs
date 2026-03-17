namespace GoalifyNow.Api.Data;

public class MacroTarget
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double Calories { get; set; }
    public double Protein { get; set; }
    public double Carbs { get; set; }
    public double Fat { get; set; }
    public bool IsTrainingDay { get; set; }
}
