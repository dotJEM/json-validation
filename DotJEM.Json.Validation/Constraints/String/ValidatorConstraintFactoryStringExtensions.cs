using System;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints.String.Length;
using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.String
{
    public static class ConstraintFactoryStringExtensions
    {
        public static CapturedConstraint Length(this IHaveConstraintFactory self, int length) => self.Capture(new ExactStringLengthJsonConstraint(length));

        public static CapturedConstraint MinLength(this IHaveConstraintFactory self, int minLength) => self.Capture(new MinStringLengthJsonConstraint(minLength));

        public static CapturedConstraint MaxLength(this IHaveConstraintFactory self, int maxLength) => self.Capture(new MaxStringLengthJsonConstraint(maxLength));

        public static CapturedConstraint LengthBetween(this IHaveConstraintFactory self, int minLength, int maxLength) => self.Capture(new StringLengthJsonConstraint(minLength, maxLength));

        public static CapturedConstraint Match(this IBeConstraintFactory self, Regex expression) => self.Capture(new MatchStringJsonConstraint(expression));

        public static CapturedConstraint Match(this IBeConstraintFactory self, string pattern, RegexOptions options = RegexOptions.Compiled) => self.Match(new Regex(pattern, options));

        public static CapturedConstraint Equal(this IBeConstraintFactory self, string value, StringComparison comparison = StringComparison.Ordinal) => self.Capture(new StringEqualsJsonConstraint(value, comparison));
    }
}