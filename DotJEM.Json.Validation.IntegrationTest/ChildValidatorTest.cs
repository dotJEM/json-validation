using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Constraints.Common;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.IntegrationTest
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
