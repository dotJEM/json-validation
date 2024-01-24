using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Comparables;

[JsonConstraintDescription("less or equal to {maxValue}")]
public class LessOrEqualToConstraint<T> : TypedJsonConstraint<T> where T : IComparable
{
    private readonly T maxValue;

    public LessOrEqualToConstraint(T maxValue)
    {
        this.maxValue = maxValue;
    }

    protected override bool Matches(T value, IJsonValidationContext context) => value.CompareTo(maxValue) < 1;
}