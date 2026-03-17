using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.WorkoutTemplates.ListTemplates;

public class ListTemplatesEndpoint(GoalifyDbContext db) : EndpointWithoutRequest<List<TemplateDto>>
{
    public override void Configure()
    {
        Get("/api/workout-templates");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var templates = await db.WorkoutTemplates.Where(t => t.UserId == userId).OrderBy(t => t.Name).ToListAsync(ct);
        var result = templates.Select(t => new TemplateDto(t.Id, t.Name, t.ExercisesJson)).ToList();
        await Send.OkAsync(result, ct);
    }
}

public sealed record TemplateDto(Guid Id, string Name, string ExercisesJson);
