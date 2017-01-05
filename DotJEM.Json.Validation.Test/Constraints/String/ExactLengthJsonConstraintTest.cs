using System;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Constraints.String.Length;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Web.Host.Test.Validation2.Constraints.String
{
    [TestFixture]
    public class ExactStringLengthJsonConstraintTest
    {
        [TestCase(42, "length equal to '42'.")]
        [TestCase(20, "length equal to '20'.")]
        public void Describe_FormatsDescription(int length, string expected)
        {
            Assert.That(new ExactStringLengthConstraint(length).Describe().ToString(), Is.EqualTo(expected));
        }

        [TestCase(42, "This string is to short.", false)]
        [TestCase(42, "This string is nearly 42 characters long.", false)]
        [TestCase(42, "This string is exactly 42 characters long.", true)]
        [TestCase(42, "This string is close to 42 characters long.", false)]
        [TestCase(42, "This string is certainly allot more than 42 characters long.", false)]
        public void Describe_FormatsDescription(int length, string str, bool expected)
        {
            Assert.That(new ExactStringLengthConstraint(length).Matches(JToken.FromObject(str), null), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class StringLengthJsonConstraintTest
    {
        [TestCase(20, 42, "length from '20' to '42'.")]
        [TestCase(20, 30, "length from '20' to '30'.")]
        public void Describe_FormatsDescription(int min, int max, string expected)
        {
            Assert.That(new StringLengthConstraint(min, max).Describe().ToString(), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class MaxStringLengthJsonConstraintTest
    {
        [TestCase(42, "length less than or equal to '42'.")]
        [TestCase(30, "length less than or equal to '30'.")]
        public void Describe_FormatsDescription(int max, string expected)
        {
            Assert.That(new MaxStringLengthConstraint(max).Describe().ToString(), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class MinStringLengthJsonConstraintTest
    {
        [TestCase(42, "length more than or equal to '42'.")]
        [TestCase(30, "length more than or equal to '30'.")]
        public void Describe_FormatsDescription(int min, string expected)
        {
            Assert.That(new MinStringLengthConstraint(min).Describe().ToString(), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class MatchStringJsonConstraintString
    {
        [TestCase("[a-z0-9].*", "match the expression: '[a-z0-9].*'.")]
        public void Describe_FormatsDescription(string regex, string expected)
        {
            Assert.That(new MatchStringConstraint(new Regex(regex, RegexOptions.Compiled)).Describe().ToString(), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class StringEqualsJsonConstraintTest
    {
        [TestCase("helloWorld", StringComparison.InvariantCulture, "equal to 'helloWorld' using InvariantCulture comparison")]
        [TestCase("helloWorld", StringComparison.Ordinal, "equal to 'helloWorld' using Ordinal comparison")]
        [TestCase("helloWorld", StringComparison.CurrentCultureIgnoreCase, "equal to 'helloWorld' using CurrentCultureIgnoreCase comparison")]
        public void Describe_FormatsDescription(string str, StringComparison comparison, string expected)
        {
            Assert.That(new StringEqualsConstraint(str, LookupComparer(comparison)).Describe().ToString(), Is.EqualTo(expected));
        }

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
