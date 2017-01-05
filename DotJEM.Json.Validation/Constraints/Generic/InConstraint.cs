using System.Collections.Generic;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Generic
{
    [JsonConstraintDescription("any of ({Values})")]
    public class InConstraint<T> : TypedJsonConstraint<T>
    {
        private readonly HashSet<T> values;

        // ReSharper disable UnusedMember.Local -> Used by description property.
        private string Values => string.Join(", ", values);
        // ReSharper restore UnusedMember.Local

        public InConstraint(IEnumerable<T> values)
        {
            this.values = new HashSet<T>(values, EqualityComparer<T>.Default);
        }

        protected override bool Matches(T value, bool wasNull, IJsonValidationContext context)
        {
            return values.Contains(value);
        }
    }

}