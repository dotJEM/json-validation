using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Constraints.Results
{
    public sealed class AndJsonConstraintResult : CompositeJsonConstraintResult
    {
        public override bool Value
        {
            get { return Results.All(r => r.Value); }
        }

        //TODO: There was some reason for this constructor, but we should try and get rid of it.
        public AndJsonConstraintResult()
            : this(new List<JsonConstraintResult>())
        {
        }

        public AndJsonConstraintResult(params JsonConstraintResult[] results)
            : this(results.ToList())
        {
        }

        public AndJsonConstraintResult(List<JsonConstraintResult> results)
            : base(results.All(r => r.Value), results)
        {
        }

        public override JsonConstraintResult Optimize()
        {
            return OptimizeAs<AndJsonConstraintResult>();
        }
    }
}