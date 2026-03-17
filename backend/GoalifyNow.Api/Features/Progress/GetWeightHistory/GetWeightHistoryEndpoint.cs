using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Progress.GetWeightHistory;

public class GetWeightHistoryEndpoint(GoalifyDbContext db) : Endpoint<WeightHistoryRequest, List<WeightPointDto>>
{
    public override void Configure()
    {
        Get("/api/progress/weight");
    }

    public override async Task HandleAsync(WeightHistoryRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var days = req.Range switch { "7d" => 7, "30d" => 30, "90d" => 90, "1y" => 365, _ => 30 };
        var since = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-days));

        var entries = await db.WeightEntries
            .Where(w => w.UserId == userId && w.Date >= since)
            .OrderBy(w => w.Date)
            .Select(w => new WeightPointDto(w.Date, w.WeightKg, w.TrendWeightKg))
            .ToListAsync(ct);

        await Send.OkAsync(entries, ct);
    }
}

public sealed record WeightHistoryRequest(string? Range);
public sealed record WeightPointDto(DateOnly Date, double WeightKg, double? TrendWeightKg);
