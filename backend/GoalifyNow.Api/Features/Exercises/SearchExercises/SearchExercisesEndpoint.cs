using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Exercises.SearchExercises;

public class SearchExercisesEndpoint(GoalifyDbContext db) : Endpoint<SearchExercisesRequest, List<ExerciseItemDto>>
{
    public override void Configure()
    {
        Get("/api/exercises");
    }

    public override async Task HandleAsync(SearchExercisesRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var query = db.Exercises.AsQueryable();

        if (!string.IsNullOrEmpty(req.Q))
            query = query.Where(e => e.Name.Contains(req.Q));

        if (!string.IsNullOrEmpty(req.Muscle))
            query = query.Where(e => e.PrimaryMuscle == req.Muscle);

        if (!string.IsNullOrEmpty(req.Equipment))
            query = query.Where(e => e.Equipment == req.Equipment);

        var exercises = await query.OrderBy(e => e.Name).Take(50).ToListAsync(ct);
        var result = exercises.Select(e => new ExerciseItemDto(e.Id, e.Name, e.PrimaryMuscle, e.SecondaryMuscles, e.Equipment, e.IsCustom)).ToList();

        await Send.OkAsync(result, ct);
    }
}

public sealed record SearchExercisesRequest(string? Q, string? Muscle, string? Equipment);
public sealed record ExerciseItemDto(Guid Id, string Name, string PrimaryMuscle, string SecondaryMuscles, string Equipment, bool IsCustom);
