using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Rules;
using DotJEM.Json.Validation.Rules.Results;
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

        public JsonRuleResult Validate(IJsonValidationContext context, JObject entity)
        {
            JsonRuleResult gr = guard.Test(context, entity);
            return !gr.Value 
                ? null 
                : rule.Test(context, entity);
        }

        public JsonFieldValidatorDescription Describe()
        {
            return new JsonFieldValidatorDescription(guard.Describe(), rule.Describe());
        }
    }
}