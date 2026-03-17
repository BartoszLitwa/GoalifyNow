using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Workouts.GetSession;

public class GetSessionEndpoint(GoalifyDbContext db) : Endpoint<GetSessionRequest, SessionDetailDto>
{
    public override void Configure()
    {
        Get("/api/workouts/{Id}");
    }

    public override async Task HandleAsync(GetSessionRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var session = await db.WorkoutSessions.FirstOrDefaultAsync(s => s.Id == req.Id && s.UserId == userId, ct);

        if (session is null) { await Send.NotFoundAsync(ct); return; }

        var exercises = await db.WorkoutExercises.Where(e => e.SessionId == session.Id).OrderBy(e => e.Order).ToListAsync(ct);
        var weIds = exercises.Select(e => e.Id).ToList();
        var sets = await db.WorkoutSets.Where(s => weIds.Contains(s.WorkoutExerciseId)).OrderBy(s => s.SetNumber).ToListAsync(ct);
        var exerciseRefIds = exercises.Select(e => e.ExerciseId).Distinct().ToList();
        var exerciseInfo = await db.Exercises.Where(e => exerciseRefIds.Contains(e.Id)).ToDictionaryAsync(e => e.Id, ct);

        var exDtos = exercises.Select(e =>
        {
            var exSets = sets.Where(s => s.WorkoutExerciseId == e.Id).Select(s => new SetDto(s.SetNumber, s.Weight, s.Reps, s.DurationSeconds, s.SetType.ToString(), s.RPE)).ToList();
            var info = exerciseInfo.GetValueOrDefault(e.ExerciseId);
            return new ExerciseDto(e.ExerciseId, info?.Name ?? "", info?.PrimaryMuscle ?? "", e.Order, e.Notes, exSets);
        }).ToList();

        var duration = session.CompletedAt.HasValue ? (int)(session.CompletedAt.Value - session.StartedAt).TotalMinutes : 0;
        await Send.OkAsync(new SessionDetailDto(session.Id, session.Name, session.StartedAt, session.CompletedAt, duration, session.Notes, exDtos), ct);
    }
}

public sealed record GetSessionRequest(Guid Id);
public sealed record SessionDetailDto(Guid Id, string Name, DateTime StartedAt, DateTime? CompletedAt, int DurationMinutes, string? Notes, List<ExerciseDto> Exercises);
public sealed record ExerciseDto(Guid ExerciseId, string Name, string Muscle, int Order, string? Notes, List<SetDto> Sets);
public sealed record SetDto(int SetNumber, double? Weight, int? Reps, int? DurationSeconds, string SetType, double? RPE);
