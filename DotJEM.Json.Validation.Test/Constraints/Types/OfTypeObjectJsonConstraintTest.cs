using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Constraints.Types;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.Test.Constraints.Types
{
    [TestFixture]
    public class OfTypeObjectJsonConstraintTest
    {

        [Test]
        public void Matches_JArray_ReturnsFalse()
        {
            JsonConstraint constraint = new OfTypeObjectConstraint();

            Assert.That(constraint.Matches(new JArray(), null), Is.False);
        }

        [Test]
        public void Matches_JObject_ReturnsTrue()
        {
            JsonConstraint constraint = new OfTypeObjectConstraint();

            Assert.That(constraint.Matches(new JObject(), null), Is.True);
        }

        [Test]
        public void Describe_ReturnsDescribtion()
        {
            JsonConstraint constraint = new OfTypeObjectConstraint();

            Assert.That(constraint.Describe().ToString(), Is.EqualTo("an object"));
        }

    }
}
