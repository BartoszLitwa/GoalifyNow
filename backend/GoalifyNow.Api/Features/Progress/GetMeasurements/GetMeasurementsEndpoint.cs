using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Progress.GetMeasurements;

public class GetMeasurementsEndpoint(GoalifyDbContext db) : EndpointWithoutRequest<List<MeasurementDto>>
{
    public override void Configure()
    {
        Get("/api/progress/measurements");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var entries = await db.BodyMeasurements
            .Where(m => m.UserId == userId)
            .OrderByDescending(m => m.Date)
            .Take(20)
            .ToListAsync(ct);

        var result = entries.Select(m => new MeasurementDto(m.Id, m.Date, m.Waist, m.Chest, m.Hips, m.BicepsL, m.BicepsR, m.ThighL, m.ThighR, m.BodyFatPct)).ToList();
        await Send.OkAsync(result, ct);
    }
}

public sealed record MeasurementDto(Guid Id, DateOnly Date, double? Waist, double? Chest, double? Hips, double? BicepsL, double? BicepsR, double? ThighL, double? ThighR, double? BodyFatPct);
