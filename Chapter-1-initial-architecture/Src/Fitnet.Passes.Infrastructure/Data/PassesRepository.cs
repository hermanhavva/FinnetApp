namespace EvolutionaryArchitecture.Fitnet.Passes.Infrastructure.Data;

using Application.Interfaces;
using Database;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

internal sealed class PassesRepository(PassesPersistence persistence) : IPassesRepository
{
    public async Task<Pass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        await persistence.Passes.FindAsync([id], cancellationToken: cancellationToken);

    public async Task AddAsync(Pass pass, CancellationToken cancellationToken = default) =>
        await persistence.Passes.AddAsync(pass, cancellationToken);

    public async Task CommitAsync(CancellationToken cancellationToken = default) =>
        await persistence.SaveChangesAsync(cancellationToken);

    public async Task<IReadOnlyList<Pass>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await persistence.Passes.AsNoTracking().ToListAsync(cancellationToken);
}
