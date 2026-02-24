namespace EvolutionaryArchitecture.Fitnet.IntegrationTests.Common.Events.EventBus.InMemory;

using EvolutionaryArchitecture.Fitnet.Common.Events;

internal sealed class TestEventFitnetConsumer : IIntegrationEventFitnetHandler<FakeEvent>
{
    public Task Handle(FakeEvent @event, CancellationToken cancellationToken)
    {
        @event.MarkAsConsumed();
        return Task.CompletedTask;
    }
}
