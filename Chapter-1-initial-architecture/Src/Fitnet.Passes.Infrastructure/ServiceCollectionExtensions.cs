namespace EvolutionaryArchitecture.Fitnet.Passes.Infrastructure;

using Data.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPassesInfrastructureLayer(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddMigrations();

        return services;
    }
}
