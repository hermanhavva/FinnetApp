namespace EvolutionaryArchitecture.Fitnet.Common.Validation.Requests;

using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class RequestValidationsExtensions
{
    public static IServiceCollection AddRequestsValidations(this IServiceCollection services, Assembly assembly) =>
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
}
