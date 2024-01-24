using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.Types;

public static class ConstraintFactoryTypesExtensions
{
    public static CapturedConstraint OfTypeString(this IBeConstraintFactory self)
        => self.Capture(new OfTypeStringConstraint());
    public static CapturedConstraint OfTypeArray(this IBeConstraintFactory self)
        => self.Capture(new OfTypeArrayConstraint());
    public static CapturedConstraint OfTypeObject(this IBeConstraintFactory self)
        => self.Capture(new OfTypeObjectConstraint());
    public static CapturedConstraint OfTypeNumber(this IBeConstraintFactory self, bool strict = true)
        => self.Capture(new OfTypeNumberConstraint(strict));
    public static CapturedConstraint OfTypeInteger(this IBeConstraintFactory self, bool strict = true)
        => self.Capture(new OfTypeIntegerConstraint(strict));
    public static CapturedConstraint OfTypeBoolean(this IBeConstraintFactory self, bool strict = true)
        => self.Capture(new OfTypeBooleanConstraint(strict));


    public static CapturedConstraint String(this IBeConstraintFactory self)
        => self.Capture(new OfTypeStringConstraint());
    public static CapturedConstraint Array(this IBeConstraintFactory self)
        => self.Capture(new OfTypeArrayConstraint());
    public static CapturedConstraint Object(this IBeConstraintFactory self)
        => self.Capture(new OfTypeObjectConstraint());
    public static CapturedConstraint Number(this IBeConstraintFactory self, bool strict = true)
        => self.Capture(new OfTypeNumberConstraint(strict));
    public static CapturedConstraint Integer(this IBeConstraintFactory self, bool strict = true)
        => self.Capture(new OfTypeIntegerConstraint(strict));
    public static CapturedConstraint Boolean(this IBeConstraintFactory self, bool strict = true)
        => self.Capture(new OfTypeBooleanConstraint(strict));

}