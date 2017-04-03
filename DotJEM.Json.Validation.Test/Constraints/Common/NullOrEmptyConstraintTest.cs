using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotJEM.Json.Validation.Constraints.Common;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.Test.Constraints.Common
{
    [TestFixture]
    public class NullOrEmptyConstraintTest
    {
        [TestCase(null, true)]
        [TestCase("", true)]
        [TestCase("   ", false)]
        [TestCase("POO", false)]
        [TestCase("  q  ", false)]
        [TestCase(42, false)]
        [TestCase(42.42, false)]
        [TestCase(true, false)]
        public void Matches_JsonValue_ReturnsExpected(object value, bool expected)
        {
            Assert.That(new NullOrEmptyConstraint().Matches(value != null ? JToken.FromObject(value) : null, null), Is.EqualTo(expected));
        }

        [TestCase("{}", true)]
        [TestCase("[]", true)]
        [TestCase("{ value: 42 }", false)]
        [TestCase("{ value: null }", false)]
        [TestCase("[42]", false)]
        [TestCase("[null]", false)]
        [TestCase("[{}]", false)]
        [TestCase("[[]]", false)]
        [TestCase("[{ value: null }]", false)]
        [TestCase("[{ value: 42 }]", false)]
        public void Matches_Json_ReturnsExpected(string value, bool expected)
        {
            Assert.That(new NullOrEmptyConstraint().Matches( JToken.Parse(value), null), Is.EqualTo(expected));
        }

    }
}
