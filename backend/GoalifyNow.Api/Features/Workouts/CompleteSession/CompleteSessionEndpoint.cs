using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Workouts.CompleteSession;

public class CompleteSessionEndpoint(GoalifyDbContext db) : Endpoint<CompleteSessionRequest, CompleteSessionResponse>
{
    public override void Configure()
    {
        Post("/api/workouts/{SessionId}/complete");
    }

    public override async Task HandleAsync(CompleteSessionRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var session = await db.WorkoutSessions.FirstOrDefaultAsync(s => s.Id == req.SessionId && s.UserId == userId, ct);

        if (session is null) { await Send.NotFoundAsync(ct); return; }

        session.CompletedAt = DateTime.UtcNow;
        var duration = session.CompletedAt.Value - session.StartedAt;

        var exercises = await db.WorkoutExercises.Where(e => e.SessionId == session.Id).ToListAsync(ct);
        var exerciseIds = exercises.Select(e => e.Id).ToList();
        var sets = await db.WorkoutSets.Where(s => exerciseIds.Contains(s.WorkoutExerciseId)).ToListAsync(ct);

        var totalVolume = sets.Sum(s => (s.Weight ?? 0) * (s.Reps ?? 0));
        var newPrs = new List<string>();

        foreach (var ex in exercises)
        {
            var exSets = sets.Where(s => s.WorkoutExerciseId == ex.Id && s.Weight.HasValue && s.Reps.HasValue);
            foreach (var s in exSets)
            {
                var existing = await db.PersonalRecords
                    .FirstOrDefaultAsync(pr => pr.UserId == userId && pr.ExerciseId == ex.ExerciseId && pr.Reps == s.Reps!.Value, ct);

                if (existing is null || s.Weight!.Value > existing.Weight)
                {
                    if (existing is not null) existing.Weight = s.Weight!.Value;
                    else db.PersonalRecords.Add(new PersonalRecord { Id = Guid.NewGuid(), UserId = userId, ExerciseId = ex.ExerciseId, Weight = s.Weight!.Value, Reps = s.Reps!.Value, AchievedAt = DateTime.UtcNow });

                    var exerciseName = await db.Exercises.Where(e => e.Id == ex.ExerciseId).Select(e => e.Name).FirstOrDefaultAsync(ct);
                    newPrs.Add($"{exerciseName}: {s.Weight}kg x {s.Reps}");
                }
            }
        }

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new CompleteSessionResponse((int)duration.TotalMinutes, exercises.Count, sets.Count, totalVolume, newPrs), ct);
    }
}

public sealed record CompleteSessionRequest(Guid SessionId);
public sealed record CompleteSessionResponse(int DurationMinutes, int ExerciseCount, int SetCount, double TotalVolume, List<string> PersonalRecords);
