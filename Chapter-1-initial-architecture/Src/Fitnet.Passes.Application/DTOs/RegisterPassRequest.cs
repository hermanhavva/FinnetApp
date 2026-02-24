namespace EvolutionaryArchitecture.Fitnet.Passes.Application.DTOs;

public sealed record RegisterPassRequest(Guid CustomerId, DateTimeOffset SignedAt, DateTimeOffset ExpireAt);
