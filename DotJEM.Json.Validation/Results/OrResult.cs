using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class OrResult : CompositeResult
    {
        public override bool IsValid => Results.Any(r => r.IsValid);

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