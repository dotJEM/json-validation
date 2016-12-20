using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Results
{
    public class RuleResult : Result 
    {
        public override bool Value => Result.Value;

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
        public override bool Value { get; }
        public string Explain { get; }

        public SimpleResult(bool value, string explain = null)
        {
            Value = value;
            Explain = explain ?? value.ToString();
        }
    }
}