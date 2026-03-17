namespace GoalifyNow.Api.Data;

public class Recipe
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Servings { get; set; } = 1;
    public string IngredientsJson { get; set; } = "[]";
    public double CaloriesPerServing { get; set; }
    public double ProteinPerServing { get; set; }
    public double CarbsPerServing { get; set; }
    public double FatPerServing { get; set; }
}
