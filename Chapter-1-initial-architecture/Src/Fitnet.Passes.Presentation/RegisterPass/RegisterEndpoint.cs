namespace EvolutionaryArchitecture.Fitnet.Passes.Presentation.RegisterPass;

using Application.DTOs;
using Common.Events;
using EvolutionaryArchitecture.Fitnet.Passes.Application.Interfaces;
using Fitnet.Contracts.SignContract.Events;

public sealed class ContractSignedEventFitnetHandler(
    IRegisterPassUseCase useCase) : IIntegrationEventFitnetHandler<ContractSignedEvent>
{
    public async Task Handle(ContractSignedEvent @event, CancellationToken cancellationToken)
    {
        // 1. Map the external event payload to the internal Use Case request
        var request = new RegisterPassRequest(
            @event.ContractCustomerId,
            @event.SignedAt,
            @event.ExpireAt);

        // 2. Delegate to the Application layer
        await useCase.ExecuteAsync(request, cancellationToken);
    }
}
