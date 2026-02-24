namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.TestEngine.IntegrationEvents.Handlers;

using EvolutionaryArchitecture.Fitnet.Common.Events;
using MediatR;

public sealed class IntegrationEventHandlerScope<TIntegrationEvent> : IDisposable
where TIntegrationEvent : IIntegrationEvent
{
    private readonly IServiceScope _serviceScope;
    internal readonly IIntegrationEventFitnetHandler<TIntegrationEvent> IntegrationEventFitnetHandler;

    public IntegrationEventHandlerScope(WebApplicationFactory<Program> applicationInMemoryFactory)
    {
        _serviceScope = applicationInMemoryFactory.Services.CreateScope();
        IntegrationEventFitnetHandler = (IIntegrationEventFitnetHandler<TIntegrationEvent>)_serviceScope
            .ServiceProvider
            .GetRequiredService<INotificationHandler<TIntegrationEvent>>();
    }

    public async Task Consume(TIntegrationEvent @event, CancellationToken cancellationToken = default) =>
        await IntegrationEventFitnetHandler.Handle(@event, cancellationToken);

    public void Dispose() =>
        _serviceScope.Dispose();
}
