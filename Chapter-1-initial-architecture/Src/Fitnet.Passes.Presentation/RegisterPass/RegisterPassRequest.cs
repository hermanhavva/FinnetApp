namespace EvolutionaryArchitecture.Fitnet.Passes.Presentation.RegisterPass;

public record RegisterPassRequest(Guid CustomerId, DateTimeOffset From, DateTimeOffset To);
