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
            JsonConstraint constraint = new OfTypeObjectJsonConstraint();

            Assert.That(constraint.Matches(null, new JArray()), Is.False);
        }

        [Test]
        public void Matches_JObject_ReturnsTrue()
        {
            JsonConstraint constraint = new OfTypeObjectJsonConstraint();

            Assert.That(constraint.Matches(null, new JObject()), Is.True);
        }

        [Test]
        public void Describe_ReturnsDescribtion()
        {
            JsonConstraint constraint = new OfTypeObjectJsonConstraint();

            Assert.That(constraint.Describe().ToString(), Is.EqualTo("of type object"));
        }

    }
}
