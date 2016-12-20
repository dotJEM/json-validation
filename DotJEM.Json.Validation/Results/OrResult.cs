using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class OrResult : CompositeResult
    {
        public override bool Value => Results.Any(r => r.Value);

        public OrResult() : base(new List<Result>())
        {
        }

        public OrResult(params Result[] results) : base(results)
        {
        }

        public OrResult(List<Result> results) : base(results)
        {
        }

        public override Result Optimize() => base.OptimizeAs<OrResult>();
    }
}