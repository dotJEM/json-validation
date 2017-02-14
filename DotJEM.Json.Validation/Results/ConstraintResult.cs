using DotJEM.Json.Validation.Constraints;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class ConstraintResult : Result
    {
        public override bool IsValid { get; }

        public JToken Token { get; }

        public JsonConstraint Constraint { get; }

        public ConstraintResult(JsonConstraint constraint, JToken token, bool value)
        {
            Constraint = constraint;
            Token = token;
            IsValid = value;
        }
    }

    public class LazyConstraintResult : Result
    {
        public override bool IsValid => Result.IsValid;

        public JToken Other { get; }
        public Result Result { get; }
        public LazyConstraint Constraint { get; }

        public LazyConstraintResult(LazyConstraint constraint, JToken other, Result result)
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