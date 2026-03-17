using System.Security.Claims;
using System.Text.Json;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Workouts.UpdateSession;

public class UpdateSessionEndpoint(GoalifyDbContext db) : Endpoint<UpdateSessionRequest, EmptyResponse>
{
    public override void Configure()
    {
        Put("/api/workouts/{SessionId}");
    }

    public override async Task HandleAsync(UpdateSessionRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var session = await db.WorkoutSessions.FirstOrDefaultAsync(s => s.Id == req.SessionId && s.UserId == userId, ct);

        if (session is null) { await Send.NotFoundAsync(ct); return; }

        if (!string.IsNullOrWhiteSpace(req.Notes)) session.Notes = req.Notes;

        if (req.Exercises is { Count: > 0 })
        {
            foreach (var ex in req.Exercises)
            {
                var we = new WorkoutExercise { Id = Guid.NewGuid(), SessionId = session.Id, ExerciseId = ex.ExerciseId, Order = ex.Order };
                db.WorkoutExercises.Add(we);

                if (ex.Sets is { Count: > 0 })
                {
                    foreach (var s in ex.Sets)
                    {
                        db.WorkoutSets.Add(new WorkoutSet
                        {
                            Id = Guid.NewGuid(), WorkoutExerciseId = we.Id, SetNumber = s.SetNumber,
                            Weight = s.Weight, Reps = s.Reps, DurationSeconds = s.DurationSeconds,
                            SetType = Enum.TryParse<SetType>(s.SetType, true, out var st) ? st : SetType.Working,
                            RPE = s.RPE
                        });
                    }
                }
            }
        }

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(ct);
    }
}

public sealed record UpdateSessionRequest(Guid SessionId, string? Notes, List<ExerciseInput>? Exercises);
public sealed record ExerciseInput(Guid ExerciseId, int Order, List<SetInput>? Sets);
public sealed record SetInput(int SetNumber, double? Weight, int? Reps, int? DurationSeconds, string SetType, double? RPE);
