using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.Types
{
    public static class ConstraintFactoryTypesExtensions
    {
        public static CapturedConstraint OfTypeString(this IBeConstraintFactory self)
            => self.Capture(new OfTypeStringJsonConstraint());
        public static CapturedConstraint OfTypeArray(this IBeConstraintFactory self)
            => self.Capture(new OfTypeArrayJsonConstraint());
        public static CapturedConstraint OfTypeObject(this IBeConstraintFactory self)
            => self.Capture(new OfTypeObjectJsonConstraint());
        public static CapturedConstraint OfTypeNumber(this IBeConstraintFactory self, bool strict = true)
            => self.Capture(new OfTypeNumberJsonConstraint(strict));
        public static CapturedConstraint OfTypeInteger(this IBeConstraintFactory self, bool strict = true)
            => self.Capture(new OfTypeIntegerJsonConstraint(strict));

    }
}
