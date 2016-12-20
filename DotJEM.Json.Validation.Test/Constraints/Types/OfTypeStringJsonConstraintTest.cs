using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Constraints.Types;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.Test.Constraints.Types
{
    [TestFixture]
    public class OfTypeStringJsonConstraintTest
    {

        [Test]
        public void Matches_JValueWithString_ReturnsTrue()
        {
            JsonConstraint constraint = new OfTypeStringConstraint();

            Assert.That(constraint.Matches(new JValue("Im a string"), null), Is.True);
        }

        [Test]
        public void Matches_JValueWithNumber_ReturnsFalse()
        {
            JsonConstraint constraint = new OfTypeStringConstraint();

            Assert.That(constraint.Matches(new JValue(42), null), Is.False);
        }

        [Test]
        public void Matches_JObject_ReturnsFalse()
        {
            JsonConstraint constraint = new OfTypeStringConstraint();

            Assert.That(constraint.Matches(new JObject(), null), Is.False);
        }

        [Test]
        public void Describe_ReturnsDescribtion()
        {
            JsonConstraint constraint = new OfTypeStringConstraint();

            Assert.That(constraint.Describe().ToString(), Is.EqualTo("of type string"));
        }

    }
}
