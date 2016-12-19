using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Results
{
    public class RuleResult : AbstractResult 
    {
        public override bool Value => Result.Value;

        public JsonRule Rule { get; }
        public AbstractResult Result { get; }

        public RuleResult(JsonRule rule, AbstractResult result)
        {
            Rule = rule;
            Result = result;
        }
    }
}