using DotJEM.Json.Validation.Constraints;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class ConstraintResult : AbstractResult
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