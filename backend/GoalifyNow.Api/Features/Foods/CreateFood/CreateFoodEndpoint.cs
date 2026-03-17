using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

namespace GoalifyNow.Api.Features.Foods.CreateFood;

public class CreateFoodEndpoint(GoalifyDbContext db) : Endpoint<CreateFoodRequest, CreateFoodResponse>
{
    public override void Configure()
    {
        Post("/api/foods");
    }

    public override async Task HandleAsync(CreateFoodRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var food = new FoodItem
        {
            Id = Guid.NewGuid(), Name = req.Name, Brand = req.Brand, Barcode = req.Barcode,
            CaloriesPer100g = req.CaloriesPer100g, ProteinPer100g = req.ProteinPer100g,
            CarbsPer100g = req.CarbsPer100g, FatPer100g = req.FatPer100g,
            ServingSize = req.ServingSize > 0 ? req.ServingSize : 100
        };
        db.FoodItems.Add(food);
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new CreateFoodResponse(food.Id), ct);
    }
}

public sealed record CreateFoodRequest(string Name, string? Brand, string? Barcode, double CaloriesPer100g, double ProteinPer100g, double CarbsPer100g, double FatPer100g, double ServingSize);
public sealed record CreateFoodResponse(Guid Id);
