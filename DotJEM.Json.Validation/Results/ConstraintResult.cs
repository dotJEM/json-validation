using DotJEM.Json.Validation.Constraints;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class ConstraintResult : Result
    {
        public override bool Value { get; }

        public JToken Token { get; }

        public JsonConstraint Constraint { get; }

        public ConstraintResult(JsonConstraint constraint, JToken token, bool value)
        {
            Constraint = constraint;
            Token = token;
            Value = value;
        }
    }
}