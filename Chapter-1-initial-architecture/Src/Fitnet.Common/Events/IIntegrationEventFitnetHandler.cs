namespace EvolutionaryArchitecture.Fitnet.Common.Events;

using MediatR;

public interface IIntegrationEventFitnetHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IIntegrationEvent;
