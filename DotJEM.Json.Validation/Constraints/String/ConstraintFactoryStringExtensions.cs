using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints.Generic;
using DotJEM.Json.Validation.Constraints.String.Length;
using DotJEM.Json.Validation.Factories;

namespace DotJEM.Json.Validation.Constraints.String
{
    public static class ConstraintFactoryStringExtensions
    {
        public static CapturedConstraint Match(this IValidatorConstraintFactory self, string pattern, RegexOptions options = RegexOptions.Compiled)
            => self.Match(new Regex(pattern, options));
        public static CapturedConstraint Match(this IValidatorConstraintFactory self, Regex expression)
            => self.Capture(new MatchStringConstraint(expression));

        public static CapturedConstraint EqualTo(this IBeConstraintFactory self, string value, StringComparison comparison = StringComparison.Ordinal)
            => self.EqualTo(value, LookupComparer(comparison));
        public static CapturedConstraint EqualTo(this IBeConstraintFactory self, string value, StringComparer comparer)
            => self.Capture(new StringEqualsConstraint(value, comparer));

        public static CapturedConstraint In(this IBeConstraintFactory self, params string[] args)
            => self.In(args.AsEnumerable());

        public static CapturedConstraint In(this IBeConstraintFactory self, StringComparison comparison, params string[] args)
            => self.In(args.AsEnumerable(), comparison);

        public static CapturedConstraint In(this IBeConstraintFactory self, StringComparer comparer, params string[] args)
            => self.In(args.AsEnumerable(), comparer);

        public static CapturedConstraint In(this IBeConstraintFactory self, IEnumerable<string> values, StringComparison comparison = StringComparison.Ordinal)
            => self.In(values, LookupComparer(comparison));

        public static CapturedConstraint In(this IBeConstraintFactory self, IEnumerable<string> values, StringComparer comparer)
            => self.Capture(new StringInConstraint(values, comparer));

        private static StringComparer LookupComparer(StringComparison comparison)
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture: return StringComparer.CurrentCulture;
                case StringComparison.CurrentCultureIgnoreCase: return StringComparer.CurrentCultureIgnoreCase;
                case StringComparison.InvariantCulture: return StringComparer.InvariantCulture;
                case StringComparison.InvariantCultureIgnoreCase: return StringComparer.InvariantCultureIgnoreCase;
                case StringComparison.Ordinal: return StringComparer.Ordinal;
                case StringComparison.OrdinalIgnoreCase: return StringComparer.OrdinalIgnoreCase;
                default:
                    throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null);
            }
        }
    }
}