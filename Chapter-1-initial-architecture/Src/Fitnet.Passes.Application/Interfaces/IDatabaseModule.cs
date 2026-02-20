namespace EvolutionaryArchitecture.Fitnet.Passes.Application.Interfaces;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public interface IDatabaseModule
{
    static abstract IServiceCollection AddDatabase(IServiceCollection services, IConfiguration configuration);

    static abstract IApplicationBuilder UseDatabase(IApplicationBuilder applicationBuilder);
}
