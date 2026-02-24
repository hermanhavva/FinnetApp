namespace EvolutionaryArchitecture.Fitnet.Passes.Application.DTOs;

public record RegisterPassRequest(Guid CustomerId, DateTimeOffset From, DateTimeOffset To);
