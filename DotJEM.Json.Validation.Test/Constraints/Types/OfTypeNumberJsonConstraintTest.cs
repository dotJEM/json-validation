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
            OfTypeNumberConstraint constraint = new OfTypeNumberConstraint(strict);
            Assert.That(constraint.Matches(new JValue(value), null), Is.EqualTo(expected));
        }

        [Test]
        public void Matches_JObject_ReturnsFalse()
        {
            OfTypeNumberConstraint constraint = new OfTypeNumberConstraint(false);
            Assert.That(constraint.Matches(new JObject(), null), Is.False);
        }

        [Test]
        public void Describe_ReturnsDescribtion()
        {
            OfTypeNumberConstraint constraint = new OfTypeNumberConstraint(false);
            Assert.That(constraint.Describe().ToString(), Is.EqualTo("of type number (strict: False)"));
        }
    }
}
