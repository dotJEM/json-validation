using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Context;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class LazyConstraintResult : Result
    {
        public override bool IsValid => Result.IsValid;

        public CompareContext Other { get; }
        public Result Result { get; }
        public LazyConstraint Constraint { get; }

        public LazyConstraintResult(LazyConstraint constraint, CompareContext other, Result result)
        {
            Constraint = constraint;
            Other = other;
            Result = result;
        }

        public override Result Optimize()
        {
            return new LazyConstraintResult(Constraint, Other, Result.Optimize());
        }
    }
}