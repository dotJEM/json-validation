using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Comparables
{
    [JsonConstraintDescription("less than {maxValue}")]
    public class LessThanConstraint<T> : TypedJsonConstraint<T> where T : IComparable
    {
        private readonly T maxValue;

        public LessThanConstraint(T maxValue)
        {
            this.maxValue = maxValue;
        }

        protected override bool Matches(T value, IJsonValidationContext context) => value.CompareTo(maxValue) < 0;
    }
}