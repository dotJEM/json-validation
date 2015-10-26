using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.Common
{
    public static class GuardConstraintFactoryCommonExtensions
    {
        public static JsonConstraint Defined(this IBeConstraintFactory self)
        {
            return new IsDefinedJsonConstraint();
        }
    }
}