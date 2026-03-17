using System.Security.Claims;
using System.Text.Json;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Recipes.CreateRecipe;

public class CreateRecipeEndpoint(GoalifyDbContext db) : Endpoint<CreateRecipeRequest, CreateRecipeResponse>
{
    public override void Configure()
    {
        Post("/api/recipes");
    }

    public override async Task HandleAsync(CreateRecipeRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var recipe = new Recipe
        {
            Id = Guid.NewGuid(), UserId = userId, Name = req.Name, Servings = req.Servings,
            IngredientsJson = JsonSerializer.Serialize(req.Ingredients),
            CaloriesPerServing = req.CaloriesPerServing, ProteinPerServing = req.ProteinPerServing,
            CarbsPerServing = req.CarbsPerServing, FatPerServing = req.FatPerServing
        };
        db.Recipes.Add(recipe);
        await db.SaveChangesAsync(ct);
        await Send.OkAsync(new CreateRecipeResponse(recipe.Id), ct);
    }
}

public sealed record CreateRecipeRequest(string Name, int Servings, List<RecipeIngredient> Ingredients, double CaloriesPerServing, double ProteinPerServing, double CarbsPerServing, double FatPerServing);
public sealed record RecipeIngredient(Guid FoodItemId, double Amount, string Unit);
public sealed record CreateRecipeResponse(Guid Id);
