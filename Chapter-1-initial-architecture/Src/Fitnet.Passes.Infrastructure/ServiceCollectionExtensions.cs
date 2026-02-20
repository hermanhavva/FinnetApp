namespace EvolutionaryArchitecture.Fitnet.Passes.Infrastructure;

using Data.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        return services;
    }

    public static IApplicationBuilder UseInfrastructureLayer(this IApplicationBuilder app)
    {
        app.UseDatabase();

        return app;
    }
}
