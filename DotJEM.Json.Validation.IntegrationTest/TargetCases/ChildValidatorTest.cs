using DotJEM.Json.Validation.Constraints.Common;
using DotJEM.Json.Validation.Constraints.String;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.IntegrationTest.TargetCases
{
    [TestFixture]
    public class ChildValidatorTest
    {
        [Test]
        public void Validate_ValidOwner_ShouldSucceed()
        {
            Assert.That(new RootValidator().Validate(JObject.Parse("{ owner: { name: 'Peter Pan' } }"), null)
                , Has.Property("IsValid").EqualTo(true));
        }

        [Test]
        public void Validate_InvalidOwner_ShouldFail()
        {
            Assert.That(new RootValidator().Validate(JObject.Parse("{ owner: { name: 'PeterPan' } }"), null)
                , Has.Property("IsValid").EqualTo(false));
        }

        [Test]
        public void Validate_NullOwner_ShouldFail()
        {
            Assert.That(new RootValidator().Validate(JObject.Parse("{ owner: null }"), null)
                , Has.Property("IsValid").EqualTo(false));
        }


        [Test]
        public void Validate_NoOwner_ShouldFail()
        {
            Assert.That(new RootValidator().Validate(JObject.Parse("{ }"), null)
                , Has.Property("IsValid").EqualTo(false));
        }

        public class RootValidator : JsonValidator
        {
            public RootValidator()
            {
                Use<ChildValidator>().For("owner");

            }
        }
        public class ChildValidator : JsonValidator
        {
            public ChildValidator()
            {
                When(Any).Then("name", Is.Required() & Must.Match("^\\w+\\s\\w+$"));
            }
        }

    }
}
