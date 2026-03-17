namespace GoalifyNow.Api.Data;

public class Exercise
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PrimaryMuscle { get; set; } = string.Empty;
    public string SecondaryMuscles { get; set; } = string.Empty;
    public string Equipment { get; set; } = string.Empty;
    public bool IsCustom { get; set; }
    public Guid? CreatedByUserId { get; set; }
}
