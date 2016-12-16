using DotJEM.Json.Validation.Constraints.Types;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.Test.Constraints.Types
{
    [TestFixture]
    public class OfTypeNumberJsonConstraintTest
    {
        [TestCase(10, true, true)]
        [TestCase("10", true, false)]
        [TestCase(10.5, true, true)]
        [TestCase("10.5", true, false)]
        [TestCase(10, false, true)]
        [TestCase(10.5, false, true)]
        [TestCase("10", false, true)]
        [TestCase("10.5", false, true)]
        public void Matches_Value_Returns(object value, bool strict, bool expected)
        {
            OfTypeNumberJsonConstraint constraint = new OfTypeNumberJsonConstraint(strict);
            Assert.That(constraint.Matches(null, new JValue(value)), Is.EqualTo(expected));
        }

        [Test]
        public void Matches_JObject_ReturnsFalse()
        {
            OfTypeNumberJsonConstraint constraint = new OfTypeNumberJsonConstraint(false);
            Assert.That(constraint.Matches(null, new JObject()), Is.False);
        }

        [Test]
        public void Describe_ReturnsDescribtion()
        {
            OfTypeNumberJsonConstraint constraint = new OfTypeNumberJsonConstraint(false);
            Assert.That(constraint.Describe().ToString(), Is.EqualTo("of type number (strict: False)"));
        }
    }
}
