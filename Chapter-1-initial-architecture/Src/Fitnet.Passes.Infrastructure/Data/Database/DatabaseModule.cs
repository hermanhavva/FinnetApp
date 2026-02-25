namespace EvolutionaryArchitecture.Fitnet.Passes.Infrastructure.Data.Database;

using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

internal static class DatabaseModule
{
    internal static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PassesPersistenceOptions>(configuration.GetSection(PassesPersistenceOptions.SectionName));
        services.AddOptionsWithValidateOnStart<PassesPersistenceOptions>();
        services.AddDbContext<PassesPersistence>((serviceProvider, options) =>
        {
            var persistenceOptions = serviceProvider.GetRequiredService<IOptions<PassesPersistenceOptions>>();
            var connectionString = persistenceOptions.Value.Passes;
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<IPassesRepository, PassesRepository>();

        return services;
    }

    internal static IServiceCollection AddMigrations(this IServiceCollection services)
    {
        services.AddHostedService<AutomaticMigrationsService>();

        return services;
    }
}
