namespace GoalifyNow.Api.Options;

public class DatabaseOptions
{
    public const string SectionName = "Database";
    public bool RunMigrations { get; set; } = true;
    public bool SeedData { get; set; } = true;
    public bool EnableDetailedErrors { get; set; }
    public bool EnableSensitiveDataLogging { get; set; }
    public int CommandTimeoutSeconds { get; set; } = 30;
}
