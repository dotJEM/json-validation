using System.Collections.Generic;
using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.Generic
{
    public static class ConstraintFactoryGenericExtensions
    {
        public static CapturedConstraint In<T>(this IBeConstraintFactory self, params T[] args)
        {
            return self.Capture(new InConstraint<T>(args));
        }
        public static CapturedConstraint In<T>(this IBeConstraintFactory self, IEnumerable<T> values)
        {
            return self.Capture(new InConstraint<T>(values));
        }

    }
}