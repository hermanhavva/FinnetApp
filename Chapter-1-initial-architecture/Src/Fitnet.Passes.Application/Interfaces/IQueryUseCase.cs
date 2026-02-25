namespace EvolutionaryArchitecture.Fitnet.Passes.Application.Interfaces;

public interface IQueryUseCase<TResponse>
{
    Task<TResponse> ExecuteAsync(CancellationToken cancellationToken = default);
}
