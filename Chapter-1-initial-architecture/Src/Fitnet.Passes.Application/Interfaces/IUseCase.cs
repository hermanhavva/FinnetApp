namespace EvolutionaryArchitecture.Fitnet.Passes.Application.Interfaces;

using System.Threading;
using System.Threading.Tasks;

public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface IUseCase<TResponse>
{
    Task<TResponse> ExecuteAsync(CancellationToken cancellationToken = default);
}
