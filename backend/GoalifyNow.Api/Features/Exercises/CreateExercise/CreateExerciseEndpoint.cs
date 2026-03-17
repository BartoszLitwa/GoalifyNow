using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Exercises.CreateExercise;

public class CreateExerciseEndpoint(GoalifyDbContext db) : Endpoint<CreateExerciseRequest, CreateExerciseResponse>
{
    public override void Configure()
    {
        Post("/api/exercises");
    }

    public override async Task HandleAsync(CreateExerciseRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var exercise = new Exercise
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            PrimaryMuscle = req.PrimaryMuscle,
            SecondaryMuscles = req.SecondaryMuscles ?? "",
            Equipment = req.Equipment ?? "",
            IsCustom = true,
            CreatedByUserId = userId
        };
        db.Exercises.Add(exercise);
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new CreateExerciseResponse(exercise.Id), ct);
    }
}

public sealed record CreateExerciseRequest(string Name, string PrimaryMuscle, string? SecondaryMuscles, string? Equipment);
public sealed record CreateExerciseResponse(Guid Id);
