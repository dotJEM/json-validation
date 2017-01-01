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
            var visitor = new DescribeFailurePathVisitor();
            string description = visitor.Describe(result);
            Console.WriteLine(description);

            string basic = descriptor.Describe(result);
            Console.WriteLine(basic);


            //Assert.That(description, Is.EqualTo("No Errors."));
        }
    }

    public class FakeValidator : JsonValidator
    {
        public FakeValidator()
        {
            //TOOD: This is not working. - One error is in the BasicJsonRule - When we select an item, if there is no items, we have a empty result.
            When(Field("gender", Is.Defined()) | Field("sex", Is.Defined())).Then(Field("gender", Must.Be.In("male", "female")) | Field("sex", Must.Be.In("male", "female")));
            //When("gender", Is.Defined()).Then(Field("gender", Must.Be.In("male", "female")) | Field("sex", Must.Be.In("male", "female")));

            //When("name", Is.Defined()).Then(It, Must.Have.LengthBetween(10, 50) & Must.Match("^[A-Za-z\\s]+$") | Have.ExactLength(5));
            //When("age", Is.Defined()).Then(It, Must.Be.Integer() & Be.GreaterThan(0));

            //When("missing", Is.Defined()).Then(It, Must.Be.Boolean());
        }
    }

    public class DescribeFailurePathVisitor : AbstractDescriptor
    {
        private bool inguard = false;

        public override void Visit(Result result)
        {
            WriteLine(result.ToString());
        }

        public override void Visit(ConstraintResult result)
        {
            WriteLine(inguard
                ? $"{result.Constraint.ContextInfo} {result.Constraint.Describe()}"
                : $"{result.Constraint.ContextInfo} {result.Constraint.Describe()} - actual value was: {result.Token ?? "NULL"}");
        }

        public override void Visit(FieldResult result)
        {
            if (!result.GuardResult.IsValid)// || result.ValidationResult.IsValid)
                return;

            inguard = true;
            Write("When ");
            result.GuardResult.Accept(this);
            inguard = false;
            Write("Then ");
            result.ValidationResult.Accept(this);
            WriteLine("");
        }

        public override void Visit(AnyResult result)
        {
            WriteLine("ANY");
        }

        public override void Visit(RuleResult result)
        {
            BasicJsonRule rule = result.Rule as BasicJsonRule;
            if (rule != null)
            {
                WriteLine($"{rule.Alias}");
            }
            base.Visit(result);
        }

        public override void Visit(FuncResult result)
        {
            Write(result.Explain);
        }

        public override void Visit(AndResult result)
        {
            List<Result> results = (inguard ? result.Results : result.Results.Where(r => !r.IsValid)).ToList();
            results = result.Results.ToList();
            if (results.Count == 0)
                return;

            if (results.Count == 1)
            {
                results.First().Accept(this);
                return;
            }
            
            WriteLine("(");
            Indent();
            bool first = true;
            foreach (Result child in result.Results)
            {
                if (!first)
                    WriteLine(" AND ");

                first = false;
                child.Accept(this);
            }
            Outdent();
            WriteLine(")");
        }


        public override void Visit(OrResult result)
        {
            WriteLine("(");
            Indent();
            bool first = true;
            foreach (Result child in result.Results)
            {
                if (!first)
                    Write(" OR ");

                first = false;
                child.Accept(this);
            }
            Outdent();
            WriteLine(")");
        }

        public override void Visit(NotResult result)
        {
            WriteLine("NOT");
            base.Visit(result);
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
            WriteLine(
                 $"{result.Constraint.ContextInfo} {result.Constraint.Describe()} - actual value was: {result.Token}");
        }
    }

    public abstract class AbstractDescriptor : JsonResultVisitor
    {
        private int indent = 0;
        private readonly StringBuilder builder = new StringBuilder();

        public string Message => builder.ToString();

        public string Describe(Result result)
        {
            result = result.Optimize();

            builder.Clear();
            result.Accept(this);
            return Message;
        }

        protected void Indent()
        {
            indent += 2;
        }

        protected void Outdent()
        {
            indent -= 2;
        }

        protected void Write(string message)
        {
            builder.Append(message);
        }

        protected void WriteLine(string message)
        {
            if (indent > 0)
                builder.AppendLine(new string(' ', indent) + message);
            else
                builder.AppendLine(message);
        }
    }
}
