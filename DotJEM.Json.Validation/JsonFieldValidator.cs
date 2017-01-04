using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Rules;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation
{
    public class JsonFieldValidator
    {
        public Rule Guard { get; }
        public Rule Rule { get; }

        public JsonFieldValidator(Rule guard, Rule rule)
        {
            Guard = guard.Optimize();
            Rule = rule.Optimize();
        }

        public Result Validate(JObject entity, IJsonValidationContext context)
        {
            Result gr = Guard.Test(entity, context);
            return !gr.IsValid 
                ? new FieldResult(this, gr, new SkippedResult())
                : new FieldResult(this, gr, Rule.Test(entity, context));
        }
    }
}