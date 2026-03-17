using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Progress.UploadPhoto;

public class UploadPhotoEndpoint(GoalifyDbContext db) : Endpoint<UploadPhotoRequest, UploadPhotoResponse>
{
    public override void Configure()
    {
        Post("/api/progress/photos");
    }

    public override async Task HandleAsync(UploadPhotoRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var photo = new ProgressPhoto
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
            PoseType = req.PoseType ?? "Front",
            StoragePath = $"photos/{userId}/{Guid.NewGuid()}.jpg",
            Notes = req.Notes
        };
        db.ProgressPhotos.Add(photo);
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new UploadPhotoResponse(photo.Id, photo.StoragePath), ct);
    }
}

public sealed record UploadPhotoRequest(string? PoseType, string? Notes);
public sealed record UploadPhotoResponse(Guid Id, string StoragePath);
