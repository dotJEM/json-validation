using System;
using System.Collections.Generic;
using DotJEM.Json.Validation.Constraints.Generic;
using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.Comparables
{
    public static class ComparableConstraintFactoryExtensions
    {
        public static CapturedConstraint LessThan<TComparable>(this IBeConstraintFactory self, TComparable value) where TComparable : IComparable
            => self.Capture(new LessThanConstraint<TComparable>(value));

        public static CapturedConstraint GreaterThan<TComparable>(this IBeConstraintFactory self, TComparable value) where TComparable : IComparable
            => self.Capture(new GreaterThanConstraint<TComparable>(value));
    }
}