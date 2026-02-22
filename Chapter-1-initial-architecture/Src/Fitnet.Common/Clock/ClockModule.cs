namespace EvolutionaryArchitecture.Fitnet.Common.Clock;

using Microsoft.Extensions.DependencyInjection;

internal static class ClockModule
{
    internal static IServiceCollection AddClock(this IServiceCollection services) =>
        services.AddSingleton(TimeProvider.System);
}
