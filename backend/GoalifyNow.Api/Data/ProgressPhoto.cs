namespace GoalifyNow.Api.Data;

public class ProgressPhoto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public string PoseType { get; set; } = "Front";
    public string StoragePath { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
