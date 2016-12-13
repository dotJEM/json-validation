using System.Collections.Generic;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Generic
{
    [JsonConstraintDescription("length must be from '{minLength}' to '{maxLength}'.")]
    public class InConstraint<T> : TypedJsonConstraint<T>
    {
        private readonly HashSet<T> values;

        public InConstraint(IEnumerable<T> values)
        {
            this.values = new HashSet<T>(values, EqualityComparer<T>.Default);
        }

        protected override bool Matches(IJsonValidationContext context, T value)
        {
            return values.Contains(value);
        }
    }
}