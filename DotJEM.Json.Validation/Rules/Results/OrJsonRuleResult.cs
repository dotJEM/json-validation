using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Rules.Results
{
    public sealed class OrJsonRuleResult : CompositeJsonRuleResult
    {
        public override bool Value => Results.Any(r => r.Value);

        public OrJsonRuleResult() 
            : base(new List<JsonRuleResult>())
        {
        }

        public OrJsonRuleResult(params JsonRuleResult[] results)
            : base(results.ToList())
        {
        }

        public OrJsonRuleResult(List<JsonRuleResult> results) 
            : base(results)
        {
        }

        public override JsonRuleResult Optimize()
        {
            return OptimizeAs<OrJsonRuleResult>();
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return JoinWriteTo(writer, result => !result.Value, "or ");
        }
    }
}