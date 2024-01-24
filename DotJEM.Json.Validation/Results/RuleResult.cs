using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Results;

public class RuleResult : Result 
{
    public override bool IsValid => Result.IsValid;

    public Rule Rule { get; }
    public Result Result { get; }

    public RuleResult(Rule rule, Result result)
    {
        Rule = rule;
        Result = result;
    }

    public override Result Optimize()
    {
        return new RuleResult(Rule, Result.Optimize());
    }
}

/// <summary>
/// Filler for simple usage of results.
/// </summary>
public class SimpleResult : Result
{
    public override bool IsValid { get; }
    public string Explain { get; }

    public SimpleResult(bool value, string explain = null)
    {
        IsValid = value;
        Explain = explain ?? value.ToString();
    }
}