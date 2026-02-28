namespace EvolutionaryArchitecture.Fitnet.Passes.Application.UseCases;

using Common.Events.EventBus;
using Contracts.Events;
using Domain.Entities;
using DTOs;
using Interfaces;

internal sealed class RegisterPassUseCase(
    IPassesRepository passesRepository,
    IEventBus eventBus) : IRegisterPassUseCase
{
    public async Task ExecuteAsync(RegisterPassRequest request, CancellationToken cancellationToken = default)
    {
        var pass = Pass.Register(request.CustomerId, request.SignedAt, request.ExpireAt);

        await passesRepository.AddAsync(pass, cancellationToken);
        await passesRepository.CommitAsync(cancellationToken);

        var passRegisteredEvent = PassRegisteredEvent.Create(pass.Id);
        await eventBus.PublishAsync(passRegisteredEvent, cancellationToken);
    }
}
