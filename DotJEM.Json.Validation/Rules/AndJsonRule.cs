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

        public override AbstractResult Test(JObject entity, IJsonValidationContext context)
        {
            //TODO: Lazy
            return Rules.Aggregate((AbstractResult)null, (result, rule) => result & rule.Test(entity, context));
        }

        public override JsonRule Optimize()
        {
            return OptimizeAs<AndJsonRule>();
        }

        //public override Description Describe()
        //{
        //    return new CompositeJsonRuleDescription(Rules.Select(rule => rule.Describe()), " and ");
        //}
    }
}