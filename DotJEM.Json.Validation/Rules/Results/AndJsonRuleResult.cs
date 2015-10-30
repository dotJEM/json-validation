using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Rules.Results
{
    public sealed class AndJsonRuleResult : CompositeJsonRuleResult
    {
        public override bool Value => Results.All(r => r.Value);

        public AndJsonRuleResult() 
            : base(new List<JsonRuleResult>())
        {
        }

        public AndJsonRuleResult(params JsonRuleResult[] results)
            : base(results.ToList())
        {
        }

        public AndJsonRuleResult(List<JsonRuleResult> results) 
            : base(results)
        {
        }

        public override JsonRuleResult Optimize()
        {
            return OptimizeAs<AndJsonRuleResult>();
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return JoinWriteTo(writer, result => true, "and ");
        }
    }
}