using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Results
{
    public class EmbededValidatorResult : Result //Result<EmbededValidatorRule>
    {
        public override bool IsValid => Result.IsValid;

        public ValidatorResult Result { get; }

        public EmbededValidatorRule Rule { get; }

        public EmbededValidatorResult(EmbededValidatorRule rule, ValidatorResult result) 
        {
            this.Rule = rule;
            this.Result = result;
        }
    }
}