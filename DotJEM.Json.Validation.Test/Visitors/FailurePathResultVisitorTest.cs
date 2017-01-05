using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotJEM.Json.Validation.Constraints.Common;
using DotJEM.Json.Validation.Constraints.Comparables;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Constraints.Types;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Rules;
using DotJEM.Json.Validation.Visitors;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace DotJEM.Json.Validation.Test.Visitors
{
    [TestFixture]
    public class DescribeFailurePathVisitorTest
    {
        [Test]
        public void Describe_NoError_ReturnsMessageWithNoError()
        {
            //Result result = new AnyResult() & new AnyResult() | new AnyResult() & !new FuncResult(false, "No");

            var validator = new FakeValidator();
            AbstractDescriptor descriptor = new BasicFakeDescriptor();

            Result result = validator.Validate(JObject.Parse("{ name: 'Peter Pan', age: -1, gender: 'mouse' }"), null);

            //var visitor = result.Accept(new DescribeFailurePathVisitor());
            string description = result.Describe();
            Console.WriteLine(description);

            string basic = descriptor.Describe(result);
            Console.WriteLine(basic);


            //Assert.That(description, Is.EqualTo("No Errors."));
        }
    }

    public class ValueDescriptor : JsonValidator
    {
        
    }

    public class FakeValidator : JsonValidator
    {
        public FakeValidator()
        {
            //TOOD: This is not working. - One error is in the BasicJsonRule - When we select an item, if there is no items, we have a empty result.
            When(Field("gender", Is.Defined()) | Field("sex", Is.Defined()))
                .Then(Field("gender", Must.Be.In("male", "female")) | Field("sex", Must.Be.In("male", "female")));
            //When("gender", Is.Defined()).Then(Field("gender", Must.Be.In("male", "female")) | Field("sex", Must.Be.In("male", "female")));

            When("name", Is.Defined()).Then(It, Must.Have.LengthBetween(10, 50) & Must.Match("^[A-Za-z\\s]+$") | Have.ExactLength(5));
            When("age", Is.Defined()).Then(It, Must.Be.Integer() & Be.GreaterThan(0));

            //When("missing", Is.Defined()).Then(It, Must.Be.Boolean());
        }
    }


    public class BasicFakeDescriptor : AbstractDescriptor
    {
        public override void Visit(Result result)
        {
            WriteLine(result.ToString());
        }

        public override void Visit(ConstraintResult result)
        {
            WriteLine($"{result.Constraint.ContextInfo} {result.Constraint.Describe()} - actual value was: {result.Token}");
        }
    }
    
}
