using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Nutrition.LogWater;

public class LogWaterEndpoint(GoalifyDbContext db) : Endpoint<LogWaterRequest, LogWaterResponse>
{
    public override void Configure()
    {
        Post("/api/nutrition/water");
    }

    public override async Task HandleAsync(LogWaterRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var date = DateOnly.FromDateTime(DateTime.UtcNow);

        db.WaterEntries.Add(new WaterEntry { Id = Guid.NewGuid(), UserId = userId, Date = date, AmountMl = req.AmountMl });
        await db.SaveChangesAsync(ct);

        var total = await db.WaterEntries.Where(w => w.UserId == userId && w.Date == date).SumAsync(w => w.AmountMl, ct);
        await Send.OkAsync(new LogWaterResponse(total), ct);
    }
}

public sealed record LogWaterRequest(int AmountMl);
public sealed record LogWaterResponse(int TotalMl);
