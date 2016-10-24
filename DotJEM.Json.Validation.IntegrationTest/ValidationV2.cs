using System;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Constraints.Common;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Factories;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.IntegrationTest
{
    [TestFixture]
    public class ValidationV2ConstraintBuilder
    {
        [Test, Ignore("Ignored while fixing Build server")]
        public void Validate_InvalidData_ShouldReturnErrors()
        {
            var constraint = (N & N) | (N & N & !N & N);

            string str = constraint.ToString();
            Assert.That(constraint, Is.TypeOf<OrJsonConstraint>());

            constraint = constraint.Optimize();

            string str2 = constraint.ToString();
            Assert.That(constraint, Is.TypeOf<OrJsonConstraint>());
        }

        
        [Test]
        public void SpecificValidator_InvalidData_ShouldReturnErrors()
        {
            TestValidator validator = new TestValidator();

            JsonValidatorResult result = validator.Validate(new JsonValidationContext(null, null), JObject.FromObject(new
            {
                test= "01234567890123456789", other="0", A = "asd"
            }));

            string rdesc = result.ToString();
            Console.WriteLine("RESULT:");
            Console.WriteLine(rdesc);

            string description = validator.Describe().ToString();
            Console.WriteLine("");
            Console.WriteLine("VALIDATOR:");
            Console.WriteLine(description);
            Assert.That(result.IsValid, Is.False);
        }

        public int counter = 1;
        public FakeJsonConstraint N => new FakeJsonConstraint("" + counter++);
    }

    public class TestValidator : JsonValidator
    {
        public TestValidator()
        {
            When(Any).Then("x", Must.Have.MinLength(3));

            When("name", x => true, "is true").Then(It, Must.Match(x => true, "be true"));

            When("name", Is.Defined()).Then("test", Must.Have.MaxLength(200));
            When("surname", Is.Defined()).Then("test", Must.Have.MaxLength(25));

            When(Field("test", Has.MinLength(5))).Then(Field("other", Should.Be.Equal("0")));

            When(Field("A", Is.Defined()) | Field("B", Is.Defined()))
                .Then(
                      Field("A", Must.Be.Equal("") | Must.Be.Equal(""))
                    & Field("B", Must.Be.Equal("")));
        }
    }


    public class FakeJsonConstraint : JsonConstraint
    {
        public string Name { get; }

        public FakeJsonConstraint(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Matches(IJsonValidationContext context, JToken token)
        {
            return true;
        }
    }
}
