using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Progress.ListPhotos;

public class ListPhotosEndpoint(GoalifyDbContext db) : EndpointWithoutRequest<List<PhotoDto>>
{
    public override void Configure()
    {
        Get("/api/progress/photos");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var photos = await db.ProgressPhotos
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.Date)
            .Take(50)
            .ToListAsync(ct);

        var result = photos.Select(p => new PhotoDto(p.Id, p.Date, p.PoseType, p.Notes)).ToList();
        await Send.OkAsync(result, ct);
    }
}

public sealed record PhotoDto(Guid Id, DateOnly Date, string PoseType, string? Notes);
