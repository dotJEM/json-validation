using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Results
{
    public class EmbededValidatorResult : Result
    {
        public override bool IsValid => Result.IsValid;

        public Result Result { get; }

        public EmbededValidatorRule Rule { get; }

        public EmbededValidatorResult(EmbededValidatorRule rule, Result result) 
        {
            this.Rule = rule;
            this.Result = result;
        }

        public override Result Optimize()
        {
            return new EmbededValidatorResult(Rule, Result.Optimize());
        }
    }
}