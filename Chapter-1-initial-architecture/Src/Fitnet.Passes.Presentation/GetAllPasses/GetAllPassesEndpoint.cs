namespace EvolutionaryArchitecture.Fitnet.Passes.Presentation.GetAllPasses;

using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal static class GetAllPassesEndpoint
{
    internal static void MapGetAllPasses(this IEndpointRouteBuilder app) =>
        app.MapGet(PassesApiPaths.GetAll, async (
                IGetAllPassesUseCase useCase,
                CancellationToken cancellationToken) =>
            {
                var response = await useCase.ExecuteAsync(cancellationToken);

                return Results.Ok(response);
            })
            .WithSummary("Returns all passes that exist in the system")
            .WithDescription("This endpoint is used to retrieve all existing passes.")
            .Produces<GetAllPassesResponse>()
            .Produces(StatusCodes.Status500InternalServerError);
}
