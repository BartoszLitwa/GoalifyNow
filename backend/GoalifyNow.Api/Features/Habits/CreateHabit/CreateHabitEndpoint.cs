using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Habits.CreateHabit;

public class CreateHabitEndpoint(GoalifyDbContext db) : Endpoint<CreateHabitRequest, CreateHabitResponse>
{
    public override void Configure()
    {
        Post("/api/habits");
    }

    public override async Task HandleAsync(CreateHabitRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);

        var habit = new Habit
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = req.Name,
            Frequency = Enum.TryParse<HabitFrequency>(req.Frequency, true, out var freq) ? freq : HabitFrequency.Daily,
            GraceDaysRemaining = 1,
            CreatedAt = DateTime.UtcNow
        };

        db.Habits.Add(habit);
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new CreateHabitResponse(habit.Id), ct);
    }
}

public sealed record CreateHabitRequest(string Name, string Frequency);
public sealed record CreateHabitResponse(Guid Id);
