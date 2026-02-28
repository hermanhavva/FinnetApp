namespace EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;

public interface IBusinessRule
{
    bool IsMet();
    string RuleError { get; }
}
