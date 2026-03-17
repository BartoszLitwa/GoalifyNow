using System.Security.Claims;
using System.Text.Json;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.WorkoutTemplates.SaveTemplate;

public class SaveTemplateEndpoint(GoalifyDbContext db) : Endpoint<SaveTemplateRequest, SaveTemplateResponse>
{
    public override void Configure()
    {
        Post("/api/workout-templates");
    }

    public override async Task HandleAsync(SaveTemplateRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var template = new WorkoutTemplate
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = req.Name,
            ExercisesJson = JsonSerializer.Serialize(req.Exercises)
        };
        db.WorkoutTemplates.Add(template);
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new SaveTemplateResponse(template.Id), ct);
    }
}

public sealed record SaveTemplateRequest(string Name, List<TemplateExercise> Exercises);
public sealed record TemplateExercise(Guid ExerciseId, int Order, int DefaultSets);
public sealed record SaveTemplateResponse(Guid Id);
