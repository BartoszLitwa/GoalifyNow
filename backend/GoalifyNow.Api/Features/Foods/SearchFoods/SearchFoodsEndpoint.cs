using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Foods.SearchFoods;

public class SearchFoodsEndpoint(GoalifyDbContext db) : Endpoint<SearchFoodsRequest, List<FoodDto>>
{
    public override void Configure()
    {
        Get("/api/foods/search");
    }

    public override async Task HandleAsync(SearchFoodsRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var query = db.FoodItems.AsQueryable();

        if (!string.IsNullOrEmpty(req.Q))
            query = query.Where(f => f.Name.Contains(req.Q));

        var foods = await query.OrderBy(f => f.Name).Take(30).ToListAsync(ct);
        var result = foods.Select(f => new FoodDto(f.Id, f.Name, f.Brand, f.Barcode, f.CaloriesPer100g, f.ProteinPer100g, f.CarbsPer100g, f.FatPer100g, f.ServingSize, f.IsVerified)).ToList();

        await Send.OkAsync(result, ct);
    }
}

public sealed record SearchFoodsRequest(string? Q);
public sealed record FoodDto(Guid Id, string Name, string? Brand, string? Barcode, double CaloriesPer100g, double ProteinPer100g, double CarbsPer100g, double FatPer100g, double ServingSize, bool IsVerified);
