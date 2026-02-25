namespace EvolutionaryArchitecture.Fitnet.Common.Documentation;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

public static class ApiDocumentationExtensions
{
    public static void UseApiDocumentation(this IEndpointRouteBuilder app) =>
        app.MapGet("/", () => Results.Redirect("/swagger"))
            .WithSummary("Documentation for the API")
            .WithDescription("This endpoint is used to redirect to the documentation for the API.")
            .Produces(StatusCodes.Status200OK)
            .WithTags("Documentation");
}
