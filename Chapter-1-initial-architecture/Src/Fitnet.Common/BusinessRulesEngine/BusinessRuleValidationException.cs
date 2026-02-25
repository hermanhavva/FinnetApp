namespace EvolutionaryArchitecture.Fitnet.Common.BusinessRulesEngine;

public class BusinessRuleValidationException(string message) : InvalidOperationException(message);
