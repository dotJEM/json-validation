using System.Linq;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public sealed class AndJsonRule : CompositeJsonRule
    {
        public AndJsonRule()
        {
        }

        public AndJsonRule(params JsonRule[] rules) 
            : base(rules)
        {
        }

        public override Result Test(JObject entity, IJsonValidationContext context)
        {
            //TODO: Lazy
            return Rules.Aggregate((Result)null, (result, rule) => result & rule.Test(entity, context));
        }

        public override JsonRule Optimize()
        {
            return OptimizeAs<AndJsonRule>();
        }
    }
}