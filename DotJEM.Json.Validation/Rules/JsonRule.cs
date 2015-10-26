using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Rules.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public abstract class JsonRule
    {
        public abstract JsonRuleResult Test(IJsonValidationContext contenxt, JObject entity);

        public static AndJsonRule operator &(JsonRule x, JsonRule y)
        {
            return new AndJsonRule(x, y);
        }

        public static OrJsonRule operator |(JsonRule x, JsonRule y)
        {
            return new OrJsonRule(x, y);
        }

        public static NotJsonRule operator !(JsonRule x)
        {
            return new NotJsonRule(x);
        }

        public virtual JsonRule Optimize()
        {
            return this;
        }

        public abstract Description Describe();
    }
}