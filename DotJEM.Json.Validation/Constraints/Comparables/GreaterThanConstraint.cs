using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Comparables
{
    [JsonConstraintDescription("greather than {minValue}")]
    public class GreaterThanConstraint<T> : TypedJsonConstraint<T> where T: IComparable
    {
        private readonly T minValue;

        public GreaterThanConstraint(T minValue)
        {
            this.minValue = minValue;
        }

        protected override bool Matches(T value, IJsonValidationContext context)
        {
            return value.CompareTo(minValue) > 0;
        }
    }
}