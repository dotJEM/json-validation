using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String.Length
{
    [JsonConstraintDescription("length more than or equal to '{minLength}'.")]
    public class MinStringLengthJsonConstraint : TypedJsonConstraint<string>
    {
        private readonly int minLength;

        public MinStringLengthJsonConstraint(int minLength)
        {
            this.minLength = minLength;
        }

        protected override bool Matches(IJsonValidationContext context, string value)
        {
            return value.Length >= minLength;
        }
    }
}