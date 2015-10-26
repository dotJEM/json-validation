using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String.Length
{
    [JsonConstraintDescription("length must be from '{minLength}' to '{maxLength}'.")]
    public class StringLengthJsonConstraint : TypedJsonConstraint<string>
    {
        private readonly int minLength;
        private readonly int maxLength;

        public StringLengthJsonConstraint(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }

        protected override bool Matches(IJsonValidationContext context, string value)
        {
            return value.Length >= minLength && value.Length <= maxLength;
        }
    }
}