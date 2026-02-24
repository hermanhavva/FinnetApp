namespace EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;

public static class BusinessRuleValidator
{
    public static void Validate(IBusinessRule rule)
    {
        if (!rule.IsMet())
        {
            throw new BusinessRuleValidationException(rule.RuleError);
        }
    }
}
