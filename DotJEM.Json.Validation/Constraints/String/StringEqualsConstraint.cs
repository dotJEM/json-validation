using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.String
{
    [JsonConstraintDescription("equal to '{value}' using {comparison} comparison")]
    public class StringEqualsConstraint : TypedJsonConstraint<string>
    {
        private readonly string value;
        private readonly string comparison;
        private readonly StringComparer comparer;
        
        public StringEqualsConstraint(string value, StringComparer comparer)
        {
            this.value = value;
            this.comparer = comparer;
            comparison = comparer.ToComparisonDescription();
        }

        protected override bool Matches(string value, IJsonValidationContext context)
        {
            return comparer.Equals(value, this.value);
        }
    }

    public static class StringComparerExtensions
    {
        public static string ToComparisonDescription(this StringComparer self)
        {
            if (self.Equals(StringComparer.Ordinal)) return "Ordinal";
            if (self.Equals(StringComparer.OrdinalIgnoreCase)) return "OrdinalIgnoreCase";
            if (self.Equals(StringComparer.InvariantCulture)) return "InvariantCulture";
            if (self.Equals(StringComparer.InvariantCultureIgnoreCase)) return "InvariantCultureIgnoreCase";
            if (self.Equals(StringComparer.CurrentCulture)) return "CurrentCulture";
            if (self.Equals(StringComparer.CurrentCultureIgnoreCase)) return "CurrentCultureIgnoreCase";
            return self.ToString();
        }
    }
}