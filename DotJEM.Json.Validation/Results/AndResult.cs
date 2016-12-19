using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class AndResult : CompositeResult
    {
        public override bool Value => Results.All(r => r.Value);

        public AndResult() : base(new List<AbstractResult>())
        {
        }

        public AndResult(params AbstractResult[] results) : base(results)
        {
        }

        public AndResult(IEnumerable<AbstractResult> results) : base(results)
        {
        }

        public override AbstractResult Optimize() => base.OptimizeAs<AndResult>();
    }
}