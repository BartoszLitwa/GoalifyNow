using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Progress.LogMeasurements;

public class LogMeasurementsEndpoint(GoalifyDbContext db) : Endpoint<LogMeasurementsRequest, LogMeasurementsResponse>
{
    public override void Configure()
    {
        Post("/api/progress/measurements");
    }

    public override async Task HandleAsync(LogMeasurementsRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var entry = new BodyMeasurement
        {
            Id = Guid.NewGuid(), UserId = userId, Date = DateOnly.FromDateTime(DateTime.UtcNow),
            Waist = req.Waist, Chest = req.Chest, Hips = req.Hips,
            BicepsL = req.BicepsL, BicepsR = req.BicepsR,
            ThighL = req.ThighL, ThighR = req.ThighR, BodyFatPct = req.BodyFatPct
        };
        db.BodyMeasurements.Add(entry);
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new LogMeasurementsResponse(entry.Id), ct);
    }
}

public sealed record LogMeasurementsRequest(double? Waist, double? Chest, double? Hips, double? BicepsL, double? BicepsR, double? ThighL, double? ThighR, double? BodyFatPct);
public sealed record LogMeasurementsResponse(Guid Id);
