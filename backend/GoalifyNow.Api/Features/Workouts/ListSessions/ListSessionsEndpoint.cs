using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Workouts.ListSessions;

public class ListSessionsEndpoint(GoalifyDbContext db) : EndpointWithoutRequest<List<SessionDto>>
{
    public override void Configure()
    {
        Get("/api/workouts");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var sessions = await db.WorkoutSessions
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.StartedAt)
            .Take(20)
            .ToListAsync(ct);

        var sessionIds = sessions.Select(s => s.Id).ToList();
        var exercises = await db.WorkoutExercises.Where(e => sessionIds.Contains(e.SessionId)).ToListAsync(ct);
        var exerciseRefIds = exercises.Select(e => e.ExerciseId).Distinct().ToList();
        var exerciseNames = await db.Exercises.Where(e => exerciseRefIds.Contains(e.Id)).ToDictionaryAsync(e => e.Id, e => e.PrimaryMuscle, ct);
        var weIds = exercises.Select(e => e.Id).ToList();
        var sets = await db.WorkoutSets.Where(s => weIds.Contains(s.WorkoutExerciseId)).ToListAsync(ct);

        var result = sessions.Select(s =>
        {
            var sExercises = exercises.Where(e => e.SessionId == s.Id).ToList();
            var sWeIds = sExercises.Select(e => e.Id).ToList();
            var sSets = sets.Where(st => sWeIds.Contains(st.WorkoutExerciseId)).ToList();
            var muscles = sExercises.Select(e => exerciseNames.GetValueOrDefault(e.ExerciseId, "")).Where(m => !string.IsNullOrEmpty(m)).Distinct().ToList();
            var volume = sSets.Sum(st => (st.Weight ?? 0) * (st.Reps ?? 0));
            var duration = s.CompletedAt.HasValue ? (int)(s.CompletedAt.Value - s.StartedAt).TotalMinutes : 0;

            return new SessionDto(s.Id, s.Name, s.StartedAt, s.CompletedAt, duration, sExercises.Count, sSets.Count, volume, muscles);
        }).ToList();

        await Send.OkAsync(result, ct);
    }
}

public sealed record SessionDto(Guid Id, string Name, DateTime StartedAt, DateTime? CompletedAt, int DurationMinutes, int ExerciseCount, int SetCount, double TotalVolume, List<string> Muscles);
