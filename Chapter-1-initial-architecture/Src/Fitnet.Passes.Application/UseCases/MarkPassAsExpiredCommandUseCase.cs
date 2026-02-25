namespace EvolutionaryArchitecture.Fitnet.Passes.Application.UseCases;

using Common.Events.EventBus;
using Contracts.Events;
using EvolutionaryArchitecture.Fitnet.Passes.Application.Interfaces;

internal sealed class MarkPassAsExpiredCommandUseCase(
    IPassesRepository passesRepository,
    TimeProvider timeProvider,
    IEventBus eventBus) : IMarkPassAsExpiredCommandUseCase
{
    public async Task<bool> ExecuteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var pass = await passesRepository.GetByIdAsync(id, cancellationToken);
        if (pass is null)
        {
            return false;
        }

        var nowDate = timeProvider.GetUtcNow();
        pass.MarkAsExpired(nowDate);

        await passesRepository.CommitAsync(cancellationToken);

        var passExpiredEvent = PassExpiredEvent.Create(pass.Id, pass.CustomerId, nowDate);
        await eventBus.PublishAsync(passExpiredEvent, cancellationToken);

        return true;
    }
}
