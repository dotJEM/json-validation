using System.Collections.Generic;
using DotJEM.Json.Validation.Constraints.Comparables;
using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.Generic;

public static class GenericConstraintFactoryExtensions
{
    public static CapturedConstraint In<T>(this IBeConstraintFactory self, params T[] args) 
        => self.Capture(new InConstraint<T>(args));
    public static CapturedConstraint In<T>(this IBeConstraintFactory self, EqualityComparer<T> comparer, params T[] args)
        => self.Capture(new InConstraint<T>(args, comparer));

    public static CapturedConstraint In<T>(this IBeConstraintFactory self, IEnumerable<T> values, EqualityComparer<T> comparer = null)
        => self.Capture(new InConstraint<T>(values, comparer));
}