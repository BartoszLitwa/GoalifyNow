using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Progress.LogWeight;

public class LogWeightEndpoint(GoalifyDbContext db) : Endpoint<LogWeightRequest, LogWeightResponse>
{
    public override void Configure()
    {
        Post("/api/progress/weight");
    }

    public override async Task HandleAsync(LogWeightRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        var previous = await db.WeightEntries
            .Where(w => w.UserId == userId)
            .OrderByDescending(w => w.Date)
            .FirstOrDefaultAsync(ct);

        var alpha = 0.1;
        var trendWeight = previous?.TrendWeightKg is not null
            ? alpha * req.WeightKg + (1 - alpha) * previous.TrendWeightKg.Value
            : req.WeightKg;

        var entry = new WeightEntry { Id = Guid.NewGuid(), UserId = userId, Date = date, WeightKg = req.WeightKg, TrendWeightKg = trendWeight };
        db.WeightEntries.Add(entry);
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(new LogWeightResponse(entry.WeightKg, trendWeight), ct);
    }
}

public sealed record LogWeightRequest(double WeightKg);
public sealed record LogWeightResponse(double WeightKg, double TrendWeightKg);
