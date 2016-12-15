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
            OfTypeArrayJsonConstraint constraint = new OfTypeArrayJsonConstraint();

            Assert.That(constraint.Matches(null, new JArray()), Is.True);
        }

        [Test]
        public void Matches_JObject_ReturnsFalse()
        {
            OfTypeArrayJsonConstraint constraint = new OfTypeArrayJsonConstraint();

            Assert.That(constraint.Matches(null, new JObject()), Is.False);
        }

        [Test]
        public void Describe_ReturnsDescribtion()
        {
            OfTypeArrayJsonConstraint constraint = new OfTypeArrayJsonConstraint();

            Assert.That(constraint.Describe().ToString(), Is.EqualTo("of type array"));
        }
    }
}
