using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Exercises.GetExerciseHistory;

public class GetExerciseHistoryEndpoint(GoalifyDbContext db) : Endpoint<GetExerciseHistoryRequest, List<ExerciseHistoryDto>>
{
    public override void Configure()
    {
        Get("/api/exercises/{ExerciseId}/history");
    }

    public override async Task HandleAsync(GetExerciseHistoryRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);

        var sessions = await db.WorkoutSessions.Where(s => s.UserId == userId && s.CompletedAt != null).Select(s => s.Id).ToListAsync(ct);
        var workoutExercises = await db.WorkoutExercises.Where(e => sessions.Contains(e.SessionId) && e.ExerciseId == req.ExerciseId).ToListAsync(ct);
        var weIds = workoutExercises.Select(e => e.Id).ToList();
        var sets = await db.WorkoutSets.Where(s => weIds.Contains(s.WorkoutExerciseId)).ToListAsync(ct);

        var sessionDates = await db.WorkoutSessions.Where(s => sessions.Contains(s.Id)).ToDictionaryAsync(s => s.Id, s => s.StartedAt, ct);
        var weSessions = workoutExercises.ToDictionary(e => e.Id, e => e.SessionId);

        var grouped = sets.GroupBy(s => weSessions.GetValueOrDefault(s.WorkoutExerciseId)).Select(g =>
        {
            var date = sessionDates.GetValueOrDefault(g.Key);
            var topSet = g.Where(s => s.Weight.HasValue).OrderByDescending(s => s.Weight!.Value * (s.Reps ?? 1)).FirstOrDefault();
            return new ExerciseHistoryDto(date, topSet?.Weight, topSet?.Reps, g.Count(), g.Sum(s => (s.Weight ?? 0) * (s.Reps ?? 0)));
        }).OrderByDescending(h => h.Date).Take(20).ToList();

        await Send.OkAsync(grouped, ct);
    }
}

public sealed record GetExerciseHistoryRequest(Guid ExerciseId);
public sealed record ExerciseHistoryDto(DateTime Date, double? TopWeight, int? TopReps, int SetCount, double Volume);
