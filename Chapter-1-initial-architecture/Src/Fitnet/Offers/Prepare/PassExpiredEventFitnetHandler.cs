namespace EvolutionaryArchitecture.Fitnet.Offers.Prepare;

using Data;
using Data.Database;
using Common.Events;
using Common.Events.EventBus;
using Passes.Contracts.Events;

internal sealed class PassExpiredEventFitnetHandler(
    IEventBus eventBus,
    OffersPersistence persistence,
    TimeProvider timeProvider) : IIntegrationEventFitnetHandler<PassExpiredEvent>
{
    public async Task Handle(PassExpiredEvent @event, CancellationToken cancellationToken)
    {
        var nowDate = timeProvider.GetUtcNow();
        var offer = Offer.PrepareStandardPassExtension(@event.CustomerId, nowDate);
        persistence.Offers.Add(offer);
        await persistence.SaveChangesAsync(cancellationToken);

        var offerPreparedEvent = OfferPrepareEvent.Create(offer.Id, offer.CustomerId, timeProvider.GetUtcNow());
        await eventBus.PublishAsync(offerPreparedEvent, cancellationToken);
    }
}
