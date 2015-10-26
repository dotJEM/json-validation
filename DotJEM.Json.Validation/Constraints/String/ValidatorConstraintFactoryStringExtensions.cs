using System;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints.String.Length;
using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.String
{
    public static class ConstraintFactoryStringExtensions
    {
        public static JsonConstraint Length(this IHaveConstraintFactory self, int length)
        {
            return new ExactStringLengthJsonConstraint(length);
        }

        public static JsonConstraint MinLength(this IHaveConstraintFactory self, int minLength)
        {
            return new MinStringLengthJsonConstraint(minLength);
        }

        public static JsonConstraint MaxLength(this IHaveConstraintFactory self, int maxLength)
        {
            return new MaxStringLengthJsonConstraint(maxLength);
        }

        public static JsonConstraint LengthBetween(this IHaveConstraintFactory self, int minLength, int maxLength)
        {
            return new StringLengthJsonConstraint(minLength, maxLength);
        }

        public static JsonConstraint Match(this IBeConstraintFactory self, Regex expression)
        {
            return new MatchStringJsonConstraint(expression);
        }

        public static JsonConstraint Match(this IBeConstraintFactory self, string pattern, RegexOptions options = RegexOptions.Compiled)
        {
            return self.Match(new Regex(pattern, options));
        }

        public static JsonConstraint Equal(this IBeConstraintFactory self, string value, StringComparison comparison = StringComparison.Ordinal)
        {
            return new StringEqualsJsonConstraint(value, comparison);
        }
    }
}