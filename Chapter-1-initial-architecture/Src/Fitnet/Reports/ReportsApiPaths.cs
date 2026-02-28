namespace EvolutionaryArchitecture.Fitnet.Reports;

using Passes.Contracts;

internal static class ReportsApiPaths
{
    private const string Reports = $"{ApiPaths.Root}/reports";
    internal const string GenerateNewReport = $"{Reports}/generate";
}
