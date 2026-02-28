namespace EvolutionaryArchitecture.Fitnet.Passes.Presentation;

using GetAllPasses;
using MarkPassAsExpired;
using Microsoft.AspNetCore.Routing;

internal static class PassesEndpoints
{
    internal static void MapPasses(this IEndpointRouteBuilder app)
    {
        app.MapGetAllPasses();
        app.MapMarkPassAsExpired();
    }
}
