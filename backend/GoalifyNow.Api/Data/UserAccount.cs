namespace GoalifyNow.Api.Data;

public enum FitnessLevel { Beginner, Intermediate, Advanced }
public enum SubscriptionTier { Free, Premium, Pro }

public class UserAccount
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public FitnessLevel FitnessLevel { get; set; } = FitnessLevel.Beginner;
    public DateOnly? DateOfBirth { get; set; }
    public bool OnboardingCompleted { get; set; }
    public SubscriptionTier SubscriptionTier { get; set; } = SubscriptionTier.Free;
    public DateTime CreatedAtUtc { get; set; }

    public static UserAccount Create(string email, string password, string role, string displayName = "")
    {
        return new UserAccount
        {
            Id = Guid.NewGuid(),
            Email = email.Trim().ToLowerInvariant(),
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Role = role,
            DisplayName = string.IsNullOrWhiteSpace(displayName) ? email.Split('@')[0] : displayName,
            CreatedAtUtc = DateTime.UtcNow
        };
    }
}
