namespace GoalifyNow.Api.Options;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Key { get; set; } = "goalifynow-super-secret-key-please-change";
    public string Issuer { get; set; } = "GoalifyNow";
    public string Audience { get; set; } = "GoalifyNow.Clients";
    public int ExpirationMinutes { get; set; } = 120;
}
