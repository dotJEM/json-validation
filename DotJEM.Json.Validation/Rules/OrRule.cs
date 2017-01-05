using System.Linq;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public sealed class OrRule : CompositeRule
    {
        public OrRule()
        {
        }

        public OrRule(params Rule[] rules)
            : base(rules)
        {
        }

        public override Result Test(JObject entity, IJsonValidationContext context)
        {
            //TODO: Lazy
            return Rules
                .Select(rule => rule.Test(entity, context))
                .Aggregate((a, b) => a | b);
        }

        public override Rule Optimize()
        {
            return OptimizeAs<OrRule>();
        }
    }
}