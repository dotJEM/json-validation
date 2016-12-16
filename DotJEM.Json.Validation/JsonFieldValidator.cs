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

        public AbstractResult Validate(JObject entity, IJsonValidationContext context)
        {
            AbstractResult gr = guard.Test(entity, context);
            return !gr.Value 
                ? null 
                : rule.Test(entity, context);
        }
    }
}