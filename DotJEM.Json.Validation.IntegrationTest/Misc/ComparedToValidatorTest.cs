using System;
using DotJEM.Json.Validation.Constraints.Common;
using DotJEM.Json.Validation.Constraints.Comparables;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.IntegrationTest
{
    [TestFixture]
    public class ComparedToValidatorTest
    {
        [Test]
        public void Validate_LeftLessThanRight_Pass()
        {
            Assert.That(new ComparedToIntegerValidator().Validate(JObject.Parse("{ left: 1, right: 2 }"), null)
                , Has.Property("IsValid").EqualTo(true));
        }

        [Test]
        public void Validate_RightEmpty_Pass()
        {
            Assert.That(new ComparedToIntegerValidator().Validate(JObject.Parse("{ left: 1, right: '' }"), null)
                , Has.Property("IsValid").EqualTo(true));
        }

        [Test]
        public void Validate_InvalidDate_Fail()
        {
            Assert.That(new ComparedToDateValidator().Validate(JObject.Parse("{ left: '2016-01-01 10:16', right: 'No Date' }"), null)
                , Has.Property("IsValid").EqualTo(false));
        }

        public class ComparedToIntegerValidator : JsonValidator
        {
            public ComparedToIntegerValidator()
            {
                When(Field("left", Has.Value()) & Field("right", Has.Value()))
                    .Then("left", ComparedTo("right", t => Must.Be.LessOrEqualTo((int) t)));
            }
        }

        public class ComparedToDateValidator : JsonValidator
        {
            public ComparedToDateValidator()
            {
                When(Field("left", Has.Value()) & Field("right", Has.Value()))
                    .Then("left", ComparedTo("right", t => Must.Be.LessOrEqualTo((DateTime)t)));
            }
        }
    }
}