namespace EvolutionaryArchitecture.Fitnet.Passes.Application;

using Interfaces;
using Microsoft.Extensions.DependencyInjection;
using UseCases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPassesInfrastructureLayer(this IServiceCollection services)
    {
        services.AddScoped<IMarkPassAsExpiredCommandUseCase, MarkPassAsExpiredCommandUseCase>();
        services.AddScoped<IGetAllPassesCommandUseCase, GetAllPassesCommandUseCase>();
        services.AddScoped<IRegisterPassUseCase, RegisterPassUseCase>();

        return services;
    }
}
