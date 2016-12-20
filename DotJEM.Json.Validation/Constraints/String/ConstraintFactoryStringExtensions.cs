using System;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints.String.Length;
using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.String
{
    public static class ConstraintFactoryStringExtensions
    {
        public static CapturedConstraint Match(this IValidatorConstraintFactory self, Regex expression)
            => self.Capture(new MatchStringConstraint(expression));

        public static CapturedConstraint Match(this IValidatorConstraintFactory self, string pattern, RegexOptions options = RegexOptions.Compiled) 
            => self.Match(new Regex(pattern, options));

        public static CapturedConstraint EqualTo(this IBeConstraintFactory self, string value, StringComparison comparison = StringComparison.Ordinal)
            => self.Capture(new StringEqualsConstraint(value, comparison));
    }
}