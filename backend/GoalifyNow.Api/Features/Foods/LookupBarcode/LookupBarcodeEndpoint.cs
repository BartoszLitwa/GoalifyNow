using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Foods.LookupBarcode;

public class LookupBarcodeEndpoint(GoalifyDbContext db) : Endpoint<LookupBarcodeRequest, FoodBarcodeDto>
{
    public override void Configure()
    {
        Get("/api/foods/barcode/{Code}");
    }

    public override async Task HandleAsync(LookupBarcodeRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var food = await db.FoodItems.FirstOrDefaultAsync(f => f.Barcode == req.Code, ct);

        if (food is null) { await Send.NotFoundAsync(ct); return; }

        await Send.OkAsync(new FoodBarcodeDto(food.Id, food.Name, food.Brand, food.CaloriesPer100g, food.ProteinPer100g, food.CarbsPer100g, food.FatPer100g, food.ServingSize), ct);
    }
}

public sealed record LookupBarcodeRequest(string Code);
public sealed record FoodBarcodeDto(Guid Id, string Name, string? Brand, double CaloriesPer100g, double ProteinPer100g, double CarbsPer100g, double FatPer100g, double ServingSize);
