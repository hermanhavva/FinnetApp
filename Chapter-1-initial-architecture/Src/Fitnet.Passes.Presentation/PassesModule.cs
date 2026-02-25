namespace EvolutionaryArchitecture.Fitnet.Passes.Presentation;

using Application;
using Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class PassesModule
{
    public static IServiceCollection AddPassesModule(this IServiceCollection services, IConfiguration configuration)
    {
        // add internal layers
        services.AddPassesApplicationLayer();
        services.AddPassesInfrastructureLayer(configuration);

        return services;
    }

    // map all endpoints
    public static IEndpointRouteBuilder MapPassesModule(this IEndpointRouteBuilder endpoints)
    {
        PassesEndpoints.MapPasses(endpoints);

        return endpoints;
    }
}
