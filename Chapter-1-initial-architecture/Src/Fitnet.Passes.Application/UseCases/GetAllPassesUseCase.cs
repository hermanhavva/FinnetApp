namespace EvolutionaryArchitecture.Fitnet.Passes.Application.UseCases;

using DTOs;
using Interfaces;

internal sealed class GetAllPassesUseCase(IPassesRepository repo) : IGetAllPassesUseCase
{
    public async Task<GetAllPassesResponse> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var passes = await repo.GetAllAsync(cancellationToken);

        var passDtos = passes
            .Select(PassDto.From)
            .ToList();

        return GetAllPassesResponse.Create(passDtos);
    }
}
