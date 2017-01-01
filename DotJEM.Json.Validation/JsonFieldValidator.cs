using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Rules;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation
{
    public class JsonFieldValidator
    {
        private readonly JsonRule guard;
        private readonly JsonRule rule;

        public JsonFieldValidator(JsonRule guard, JsonRule rule)
        {
            this.guard = guard.Optimize();
            this.rule = rule.Optimize();
        }

        public Result Validate(JObject entity, IJsonValidationContext context)
        {
            Result gr = guard.Test(entity, context);
            return !gr.IsValid 
                ? new FieldResult(this, gr, new SkippedResult())
                : new FieldResult(this, gr, rule.Test(entity, context));
        }
    }
}