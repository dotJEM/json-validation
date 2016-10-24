using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Factories;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common
{
    public static class CommonConstraintFactoryExtensions
    {
        public static CapturedConstraint Defined(this IBeConstraintFactory self)
        {
            return self.Capture(new IsDefinedJsonConstraint());
        }

        public static CapturedConstraint Matching(this IGuardConstraintFactory self, Func<JToken, bool> constraintFunc, string explain)
        {
            return self.Capture(new FuncJsonConstraint((c, t) => constraintFunc(t), explain));
        }
        
        public static CapturedConstraint Matching(this IGuardConstraintFactory self, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return self.Capture(new FuncJsonConstraint(constraintFunc, explain));
        }

        public static CapturedConstraint Match(this IValidatorConstraintFactory self, Func<JToken, bool> constraintFunc, string explain)
        {
            return self.Capture(new FuncJsonConstraint((c, t) => constraintFunc(t), explain));
        }

        public static CapturedConstraint Match(this IValidatorConstraintFactory self, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return self.Capture(new FuncJsonConstraint(constraintFunc, explain));
        }
        
    }
}