using System;
using System.Data;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Constraints.Common;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Results;
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

            JsonValidatorResult result = validator.Validate(JObject.FromObject(new
            {
                test= "01234567890123456789", other="0", a = "asd", sub = new  { name = "Foo", type = "##Bar" }
            }), new JsonValidationContext());

            string rdesc = result.ToString();
            Console.WriteLine("RESULT:");
            Console.WriteLine(rdesc);

            Assert.That(result.IsValid, Is.False);
            //string description = validator.Describe().ToString();
            //Console.WriteLine("");
            //Console.WriteLine("VALIDATOR:");
            //Console.WriteLine(description);
            //
        }

        public int counter = 1;
        public FakeJsonConstraint N => new FakeJsonConstraint("" + counter++);
    }

    public class TestValidator : JsonValidator
    {
        public TestValidator()
        {
            When(Any).Then("test", Must.Have.LengthBetween(16,32));
            When("other", x => (string)x == "0", "When other is 0").Then(Field("a", Must.Match("\\w{3}")));
            Use<ChildValidator>().For("sub");
            //When(Any).Then("x", Must.Have.MinLength(3));

            //When(Any).Then("something", Must.Match(token => (bool?)token == true, "somthing must be boolean and true!"));
            //When("name", x => true, "is true").Then(It, Must.Match(x => true, "be true"));
            //When(Field("name", x => true, "is true")).Then(It.MustHave);

            //When(Any).Use<ChildValidator>().For(It);
            //When(Any).Use<ChildValidator>().For("ASDASDASD");
            //When(Any).Use<ChildValidator>().For(Each(""));

            //NOTE: Old Syntax proposal
            //dynamic s = this;
            //s.For("items", s.Use<TestValidator>());
            //s.When("items").Then(s.For("items").Use<TestValidator>());

            //NOTE: Syntax proposal
            //s.Use<TestValidator>().For(Field("x"));
            //s.Use<TestValidator>().For(All("items"));

            //NOTE: Syntax proposal
            //s.When("x", Is.Defined()).Use<TestValidator>().For(It);
            //s.When("x", Is.Defined()).Use<TestValidator>().For(Field("x"));
            //s.When("x", Is.Defined()).Use<TestValidator>().For(All("items"));

            //When(x => true);

            //NOTE: Old Syntax proposal
            //s.ForEach("items",
            //    When("name", Is.LengthBetween(2,5)).Then(It, Must.Match("[A-Z]{2}\\d{3}")),
            //    When("name", Is.LengthBetween(6, 7)).Then(It, Must.Match("[A-Z]{2}\\d{3}\\-Z[1-6]"))
            //    );

            //NOTE: Syntax proposal
            //When(Any).Then(All("", Should.Have.Length(42)));
            //When(Any).Then(Some("", Should.Have.Length(42)));
            //When(Any).Then(None("", Should.Have.Length(42)));

            //When validating content in arrays, the framework should support some additional hooks:
            // - For each item in the array, if the item matches rule A then it must fulfill rule B (contextual validation)
            // - When A contains X elements(or another broad precondition) each element MUST...
            // - Some Elements must...
            // - At least one element must...
            //etc.

            //When("name", Is.Defined()).Then("test", Must.Have.MaxLength(200));
            //When("surname", Is.Defined()).Then("test", Must.Have.MaxLength(25));

            //When(Any).Then("$created", (Must.Be.DateTime() & Must.Be.DateAfter(new DateTime(2000, 1, 1))) | Must.Match("akshdakshd"));

            //When("x", x => (DateTime)x == DateTime.Now, "").Then(Must.Be.DateLessThan(Field("y")));

            //When(Any).Then("$updated", Must.Match(x => x.));

            // When Any, Then A must be greter than field B
            // When Any, Then A compared to B, must be greater than B

            //NOTE: Syntax Sugestion for using one field to validate another in a non-static way.
            //When(Any).Then(Field("$updated", ComparedTo("B", b => Must.Be.GreaterThan(b)) ));

            //When(Field("test", Has.MinLength(5)))
            //    .Then(Field("other", Should.Be.Equal("0"))
            //        | Field("other", Should.Be.Equal("1")));

            //When(Field("test", Has.MinLength(5)))
            //    .Then(Field("other", Should.Be.Equal("0") | Should.Be.Equal("1")));


            //When(Field("A", Is.Defined()) | Field("B", Is.Defined()))
            //    .Then(
            //          Field("A", Must.Be.Equal("") | Must.Be.Equal(""))
            //        & Field("B", Must.Match(".*")));



        }
    }

    public class ChildValidator : JsonValidator
    {
        public ChildValidator()
        {
            When("name", Is.Equal("Foo", StringComparison.OrdinalIgnoreCase)).Then("type", Should.Match("^\\w+$"));
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
