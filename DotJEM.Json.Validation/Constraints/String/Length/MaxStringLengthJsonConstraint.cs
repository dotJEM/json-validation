using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String.Length
{
    [JsonConstraintDescription("length less than or equal to '{maxLength}'.")]
    public class MaxStringLengthJsonConstraint : TypedJsonConstraint<string>
    {
        private readonly int maxLength;

        public MaxStringLengthJsonConstraint(int maxLength)
        {
            this.maxLength = maxLength;
        }

        protected override bool Matches(IJsonValidationContext context, string value)
        {
            return value.Length <= maxLength;
        }
    }
}