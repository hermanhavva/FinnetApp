namespace EvolutionaryArchitecture.Fitnet.Passes.Application.DTOs;

using Domain.Entities;

public record GetAllPassesResponse(IReadOnlyCollection<PassDto> Passes)
{
    internal static GetAllPassesResponse Create(IReadOnlyCollection<PassDto> passes) => new(passes);
}

public record PassDto(Guid Id, Guid CustomerId)
{
    public static PassDto From(Pass contract) => new(contract.Id, contract.CustomerId);
}
