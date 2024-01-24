using System.Collections.Generic;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Generic;

[JsonConstraintDescription("any of ({Values})")]
public class InConstraint<T> : TypedJsonConstraint<T>
{
    private readonly HashSet<T> values;

    // ReSharper disable UnusedMember.Local -> Used by description property.
#pragma warning disable IDE0051
    private string Values => string.Join(", ", values);
#pragma warning restore IDE0051
    // ReSharper restore UnusedMember.Local

    public InConstraint(IEnumerable<T> values, IEqualityComparer<T> comparer = null)
    {
        this.values = new(values, comparer ?? EqualityComparer<T>.Default);
    }

    protected override bool Matches(T value, bool wasNull, IJsonValidationContext context)
    {
        return values.Contains(value);
    }
}