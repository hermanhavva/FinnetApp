namespace EvolutionaryArchitecture.Fitnet.Passes.Presentation.MarkPassAsExpired;

using Application.Interfaces;
using EvolutionaryArchitecture.Fitnet.Passes.Presentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class MarkPassAsExpiredEndpoint
{
    internal static void MapMarkPassAsExpired(this IEndpointRouteBuilder app) => app.MapPatch(
            PassesApiPaths.MarkPassAsExpired,
            async (
                Guid id,
                IMarkPassAsExpiredCommandUseCase commandUseCase,
                CancellationToken cancellationToken) =>
            {
                var wasFoundAndMarked = await commandUseCase.ExecuteAsync(id, cancellationToken);

                return wasFoundAndMarked ? Results.NoContent() : Results.NotFound();
            })
        .WithSummary("Marks pass which expired")
        .WithDescription("This endpoint is used to mark expired pass. Based on that it is possible to offer new contract to customer.")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status500InternalServerError);
}
