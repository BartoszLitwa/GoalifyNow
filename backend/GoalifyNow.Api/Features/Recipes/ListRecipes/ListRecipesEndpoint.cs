using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Recipes.ListRecipes;

public class ListRecipesEndpoint(GoalifyDbContext db) : EndpointWithoutRequest<List<RecipeDto>>
{
    public override void Configure()
    {
        Get("/api/recipes");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var recipes = await db.Recipes.Where(r => r.UserId == userId).OrderBy(r => r.Name).ToListAsync(ct);
        var result = recipes.Select(r => new RecipeDto(r.Id, r.Name, r.Servings, r.CaloriesPerServing, r.ProteinPerServing, r.CarbsPerServing, r.FatPerServing)).ToList();
        await Send.OkAsync(result, ct);
    }
}

public sealed record RecipeDto(Guid Id, string Name, int Servings, double CaloriesPerServing, double ProteinPerServing, double CarbsPerServing, double FatPerServing);
