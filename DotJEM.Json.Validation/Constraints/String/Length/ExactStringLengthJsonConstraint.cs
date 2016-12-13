using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String.Length
{
    [JsonConstraintDescription("length equal to '{length}'.")]
    public class ExactStringLengthJsonConstraint : TypedJsonConstraint<string>
    {
        private readonly int length;

        public ExactStringLengthJsonConstraint(int length)
        {
            this.length = length;
        }

        protected override bool Matches(IJsonValidationContext context, string value)
        {
            return value.Length == length;
        }
    }
}