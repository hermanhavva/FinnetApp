namespace EvolutionaryArchitecture.Fitnet.Passes.Presentation;

using Contracts;

public static class PassesApiPaths
{
    public const string GetAll = $"{ApiPaths.Root}/passes";
    internal const string MarkPassAsExpired = $"{ApiPaths.Root}/passes/{{id}}";
}
