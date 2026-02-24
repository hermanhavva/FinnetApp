namespace EvolutionaryArchitecture.Fitnet.Passes.Application.Interfaces;

using Domain.Entities;

public interface IPassesRepository
{
    Task<Pass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Pass pass, CancellationToken cancellationToken = default);
    Task CommitAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Pass>> GetAllAsync(CancellationToken cancellationToken = default);
}
