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
}