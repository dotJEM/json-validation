using System;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Constraints.String.Length;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Web.Host.Test.Validation2.Constraints.String
{
    [TestFixture]
    public class ExactStringLengthJsonConstraintTest
    {
        [TestCase(42, "length must be '42'.")]
        [TestCase(20, "length must be '20'.")]
        public void Describe_FormatsDescription(int length, string expected)
        {
            Assert.That(new ExactStringLengthJsonConstraint(length).Describe(null, null).ToString(), Is.EqualTo(expected));
        }

        [TestCase(42, "This string should be 42 characters long..", true)]
        [TestCase(42, "This string is certainly not 42 characters long..", false)]
        public void Describe_FormatsDescription(int length, string str, bool expected)
        {
            Assert.That(new ExactStringLengthJsonConstraint(length).Matches(null, JToken.FromObject(str)), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class StringLengthJsonConstraintTest
    {
        [TestCase(20, 42, "length must be from '20' to '42'.")]
        [TestCase(20, 30, "length must be from '20' to '30'.")]
        public void Describe_FormatsDescription(int min, int max, string expected)
        {
            Assert.That(new StringLengthJsonConstraint(min, max).Describe(null, null).ToString(), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class MaxStringLengthJsonConstraintTest
    {
        [TestCase(42, "length must be less than or equal to '42'.")]
        [TestCase(30, "length must be less than or equal to '30'.")]
        public void Describe_FormatsDescription(int max, string expected)
        {
            Assert.That(new MaxStringLengthJsonConstraint(max).Describe(null, null).ToString(), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class MinStringLengthJsonConstraintTest
    {
        [TestCase(42, "length must be more than or equal to '42'.")]
        [TestCase(30, "length must be more than or equal to '30'.")]
        public void Describe_FormatsDescription(int min, string expected)
        {
            Assert.That(new MinStringLengthJsonConstraint(min).Describe(null, null).ToString(), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class MatchStringJsonConstraintString
    {
        [TestCase("[a-z0-9].*", "match the expression: '[a-z0-9].*'.")]
        public void Describe_FormatsDescription(string regex, string expected)
        {
            Assert.That(new MatchStringJsonConstraint(new Regex(regex, RegexOptions.Compiled)).Describe(null, null).ToString(), Is.EqualTo(expected));
        }
    }

    [TestFixture]
    public class StringEqualsJsonConstraintTest
    {
        [TestCase("helloWorld", StringComparison.InvariantCulture, "be equal to 'helloWorld' (InvariantCulture).")]
        [TestCase("helloWorld", StringComparison.Ordinal, "be equal to 'helloWorld' (Ordinal).")]
        [TestCase("helloWorld", StringComparison.CurrentCultureIgnoreCase, "be equal to 'helloWorld' (CurrentCultureIgnoreCase).")]
        public void Describe_FormatsDescription(string str, StringComparison comparison, string expected)
        {
            Assert.That(new StringEqualsJsonConstraint(str, comparison).Describe(null, null).ToString(), Is.EqualTo(expected));
        }
    }


}
