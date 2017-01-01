using System.Linq;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public sealed class OrJsonRule : CompositeJsonRule
    {
        public OrJsonRule()
        {
        }

        public OrJsonRule(params JsonRule[] rules)
            : base(rules)
        {
        }

        public override Result Test(JObject entity, IJsonValidationContext context)
        {
            //TODO: Lazy
            return Rules
                .Select(rule => rule.Test(entity, context))
                .Aggregate((a, b) =>
                {
                    return a | b;
                });
        }

        public override JsonRule Optimize()
        {
            return OptimizeAs<OrJsonRule>();
        }
    }
}