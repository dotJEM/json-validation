using System;
using System.Collections.Generic;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String
{
    [JsonConstraintDescription("any of ({Values}) using {comparison} comparison")]
    public class StringInConstraint : TypedJsonConstraint<string>
    {
        private readonly HashSet<string> values;

        private readonly string comparison;

        // ReSharper disable UnusedMember.Local -> Used by description property.
        private string Values => string.Join(", ", values);
        // ReSharper restore UnusedMember.Local

        public StringInConstraint(IEnumerable<string> values, StringComparer comparer)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));

            this.values = new HashSet<string>(values, comparer);
            comparison = comparer.ToComparisonDescription();
        }

        protected override bool Matches(string value, bool wasNull, IJsonValidationContext context)
        {
            return values.Contains(value);
        }
    }

}