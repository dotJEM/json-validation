using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Results
{
    public class RuleResult : Result 
    {
        public override bool IsValid => Result.IsValid;

        public JsonRule Rule { get; }
        public Result Result { get; }

        public RuleResult(JsonRule rule, Result result)
        {
            Rule = rule;
            Result = result;
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
}