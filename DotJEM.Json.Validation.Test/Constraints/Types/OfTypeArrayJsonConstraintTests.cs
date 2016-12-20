using DotJEM.Json.Validation.Constraints.Types;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.Test.Constraints.Types
{
    [TestFixture]
    public class OfTypeArrayJsonConstraintTest
    {
        [Test]
        public void Matches_JArray_ReturnsTrue()
        {
            OfTypeArrayConstraint constraint = new OfTypeArrayConstraint();

            Assert.That(constraint.Matches(new JArray(), null), Is.True);
        }

        [Test]
        public void Matches_JObject_ReturnsFalse()
        {
            OfTypeArrayConstraint constraint = new OfTypeArrayConstraint();

            Assert.That(constraint.Matches(new JObject(), null), Is.False);
        }

        [Test]
        public void Describe_ReturnsDescribtion()
        {
            OfTypeArrayConstraint constraint = new OfTypeArrayConstraint();

            Assert.That(constraint.Describe().ToString(), Is.EqualTo("of type array"));
        }
    }
}
