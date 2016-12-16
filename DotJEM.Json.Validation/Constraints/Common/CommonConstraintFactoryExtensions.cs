using System;
using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Constraints.Generic;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Factories;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common
{
    public static class CommonConstraintFactoryExtensions
    {
        public static CapturedConstraint Defined(this IBeConstraintFactory self) => self.Capture(new IsDefinedJsonConstraint());

        public static CapturedConstraint In<T>(this IBeConstraintFactory self, IEnumerable<T> elements) => self.Capture(new InConstraint<T>(elements));

        public static CapturedConstraint In<T>(this IBeConstraintFactory self, params T[] elements) => self.In(elements.AsEnumerable());

        public static CapturedConstraint Matching(this IGuardConstraintFactory self, Func<JToken, bool> constraintFunc, string explain) => self.Capture(new FuncJsonConstraint((c, t) => constraintFunc(t), explain));

        public static CapturedConstraint Matching(this IGuardConstraintFactory self, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain) => self.Capture(new FuncJsonConstraint(constraintFunc, explain));

        public static CapturedConstraint Match(this IValidatorConstraintFactory self, Func<JToken, bool> constraintFunc, string explain) => self.Capture(new FuncJsonConstraint((c, t) => constraintFunc(t), explain));

        public static CapturedConstraint Match(this IValidatorConstraintFactory self, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain) => self.Capture(new FuncJsonConstraint(constraintFunc, explain));
    }
}