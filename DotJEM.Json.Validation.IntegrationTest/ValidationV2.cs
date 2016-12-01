using System;
using System.Data;
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
            //When(Field("name", x => true, "is true")).Then(It.MustHave);

            dynamic s = this;

            s.For("items", s.Use<TestValidator>());
            s.When("items").Then(s.For("items").Use<TestValidator>());

            When(x => true);

            s.ForEach("items",
                When("name", Is.LengthBetween(2,5)).Then(It, Must.Match("[A-Z]{2}\\d{3}")),
                When("name", Is.LengthBetween(6, 7)).Then(It, Must.Match("[A-Z]{2}\\d{3}\\-Z[1-6]"))
                );

            //When validating content in arrays, the framework should support some additional hooks:
            // - For each item in the array, if the item matches rule A then it must fulfill rule B (contextual validation)
            // - When A contains X elements(or another broad precondition) each element MUST...
            // - Some Elements must...
            // - At least one element must...
            //etc.

            When("name", Is.Defined()).Then("test", Must.Have.MaxLength(200));
            When("surname", Is.Defined()).Then("test", Must.Have.MaxLength(25));

            When(Field("test", Has.MinLength(5))).Then(Field("other", Should.Be.Equal("0")));

            When(Field("A", Is.Defined()) | Field("B", Is.Defined()))
                .Then(
                      Field("A", Must.Be.Equal("") | Must.Be.Equal(""))
                    & Field("B", Must.Be.Equal("")));


        }
    }

    public class ChildValidator : JsonValidator
    {
        public ChildValidator()
        {
            When("")
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
