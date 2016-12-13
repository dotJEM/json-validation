using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String
{
    [JsonConstraintDescription("equal to '{value}' ({comparison}).")]
    public class StringEqualsJsonConstraint : TypedJsonConstraint<string>
    {
        private readonly string value;
        private readonly StringComparison comparison;
        
        public StringEqualsJsonConstraint(string value, StringComparison comparison = StringComparison.Ordinal)
        {
            this.value = value;
            this.comparison = comparison;
        }

        protected override bool Matches(IJsonValidationContext context, string value)
        {
            return value.Equals(this.value, comparison);
        }
    }
}