using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Results
{
    public sealed class OrJsonConstraintResult : CompositeJsonConstraintResult
    {
        public override bool Value
        {
            get { return Results.Any(r => r.Value); }
        }

         //TODO: There was some reason for this constructor, but we should try and get rid of it.
        public OrJsonConstraintResult()
            : this(new List<JsonConstraintResult>())
        {
        }

        public OrJsonConstraintResult(params JsonConstraintResult[] results)
            : this(results.ToList())
        {
        }

        public OrJsonConstraintResult(List<JsonConstraintResult> results) 
            : base(results.Any(r => r.Value), results)
        {
        }

        public override JsonConstraintResult Optimize()
        {
            return OptimizeAs<OrJsonConstraintResult>();
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return JoinWriteTo(writer, result => true, "or ");
        }
    }
}