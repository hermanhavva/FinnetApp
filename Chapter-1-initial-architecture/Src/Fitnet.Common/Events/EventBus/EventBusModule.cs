namespace EvolutionaryArchitecture.Fitnet.Common.Events.EventBus;

using System.Reflection;
using InMemory;
using Microsoft.Extensions.DependencyInjection;

internal static class EventBusModule
{
    internal static IServiceCollection AddEventBus(this IServiceCollection services) =>
        services.AddInMemoryEventBus(Assembly.GetExecutingAssembly());
}
