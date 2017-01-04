using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public abstract class Rule
    {
        public abstract Result Test(JObject entity, IJsonValidationContext contenxt);
        
        public static AndRule operator &(Rule x, Rule y)
        {
            return new AndRule(x, y);
        }

        public static OrRule operator |(Rule x, Rule y)
        {
            return new OrRule(x, y);
        }

        public static NotRule operator !(Rule x)
        {
            return new NotRule(x);
        }

        public virtual Rule Optimize()
        {
            return this;
        }
    }
}