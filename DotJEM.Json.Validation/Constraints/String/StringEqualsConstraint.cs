using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.String
{
    [JsonConstraintDescription("equal to '{value}' ({comparison}).")]
    public class StringEqualsConstraint : TypedJsonConstraint<string>
    {
        private readonly string value;
        private readonly StringComparison comparison;
        
        public StringEqualsConstraint(string value, StringComparison comparison = StringComparison.Ordinal)
        {
            this.value = value;
            this.comparison = comparison;
        }

        protected override bool Matches(string value, IJsonValidationContext context)
        {
            return value.Equals(this.value, comparison);
        }
    }


}