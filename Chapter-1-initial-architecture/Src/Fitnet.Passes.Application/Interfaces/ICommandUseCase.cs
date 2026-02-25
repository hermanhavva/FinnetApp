namespace EvolutionaryArchitecture.Fitnet.Passes.Application.Interfaces;

using System.Threading;
using System.Threading.Tasks;

public interface ICommandUseCase<in TRequest, TResponse>
{
    Task<TResponse> ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
}

public interface ICommandUseCase<in TRequest>
{
    Task ExecuteAsync(TRequest request, CancellationToken cancellationToken = default);
}
