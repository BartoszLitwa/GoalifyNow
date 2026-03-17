namespace GoalifyNow.Api.Data;

public class UserPreference
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string WeightUnit { get; set; } = "kg";
    public string DistanceUnit { get; set; } = "km";
    public bool DarkMode { get; set; }
    public string EnabledModules { get; set; } = "[]";
    public string SelectedGoals { get; set; } = "[]";
}
