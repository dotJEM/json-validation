using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class OrResult : CompositeResult
    {
        public override bool Value => Results.Any(r => r.Value);

        public OrResult() : base(new List<AbstractResult>())
        {
        }

        public OrResult(params AbstractResult[] results) : base(results)
        {
        }

        public OrResult(List<AbstractResult> results) : base(results)
        {
        }

        public override AbstractResult Optimize() => base.OptimizeAs<OrResult>();
    }
}