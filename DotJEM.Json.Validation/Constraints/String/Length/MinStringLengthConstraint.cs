using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String.Length
{
    [JsonConstraintDescription("length more than or equal to '{minLength}'.")]
    public class MinStringLengthConstraint : TypedJsonConstraint<string>
    {
        private readonly int minLength;

        public MinStringLengthConstraint(int minLength)
        {
            this.minLength = minLength;
        }

        protected override bool Matches(string value, IJsonValidationContext context)
        {
            return value.Length >= minLength;
        }
    }
}