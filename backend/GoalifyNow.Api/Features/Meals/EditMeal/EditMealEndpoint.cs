using System.Security.Claims;

using GoalifyNow.Api.Data;

using FastEndpoints;

using Microsoft.EntityFrameworkCore;

namespace GoalifyNow.Api.Features.Meals.EditMeal;

public class EditMealEndpoint(GoalifyDbContext db) : Endpoint<EditMealRequest, EmptyResponse>
{
    public override void Configure()
    {
        Put("/api/meals/{Id}");
    }

    public override async Task HandleAsync(EditMealRequest req, CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdClaim)) { await Send.UnauthorizedAsync(ct); return; }
        var userId = Guid.Parse(userIdClaim);
        var meal = await db.Meals.FirstOrDefaultAsync(m => m.Id == req.Id && m.UserId == userId, ct);

        if (meal is null) { await Send.NotFoundAsync(ct); return; }

        if (!string.IsNullOrEmpty(req.MealType) && Enum.TryParse<MealType>(req.MealType, true, out var mt))
            meal.MealType = mt;

        await db.SaveChangesAsync(ct);
        await Send.OkAsync(ct);
    }
}

public sealed record EditMealRequest(Guid Id, string? MealType);
