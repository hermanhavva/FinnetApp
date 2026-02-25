namespace EvolutionaryArchitecture.Fitnet.Passes.Presentation.RegisterPass;

using Application.DTOs;
using Common.Events;
using EvolutionaryArchitecture.Fitnet.Passes.Application.Interfaces;

public sealed class ContractSignedEventFitnetHandler(
    IRegisterPassUseCase useCase) : IIntegrationEventFitnetHandler<ContractSignedEvent>
{
    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        var request = new RegisterPassRequest(
            @event.ContractCustomerId,
            @event.SignedAt,
            @event.ExpireAt);

        await useCase.ExecuteAsync(request, cancellationToken);
    }
}
