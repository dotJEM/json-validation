using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotJEM.Json.Validation.Constraints.Common;
using DotJEM.Json.Validation.Constraints.Comparables;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Constraints.Types;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Rules;
using DotJEM.Json.Validation.Visitors;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using static System.Environment;

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

            Result result = validator.Validate(JObject.Parse("{ name: 'Peter Pan', age: -1 }"), null);

            //var visitor = result.Accept(new DescribeFailurePathVisitor());
            var visitor = new DescribeFailurePathVisitor();
            string description = visitor.Describe(result);
            Console.WriteLine(description);
            //Assert.That(description, Is.EqualTo("No Errors."));
        }
    }

    public class FakeValidator : JsonValidator
    {
        public FakeValidator()
        {
            When("name", Is.Defined()).Then(It, Must.Have.LengthBetween(10, 50) & Must.Match("^[A-Za-z\\s]+$"));
            When("age", Is.Defined()).Then(It, Must.Be.Integer() & Be.GreaterThan(0));

            When("missing", Is.Defined()).Then(It, Must.Be.Boolean());
        }
    }

    public class DescribeFailurePathVisitor : JsonResultVisitor
    {
        private int indent = 0;
        private bool inguard = false;
        private readonly StringBuilder builder = new StringBuilder();
        public string Message => builder.ToString();
        
        private void Write(string message)
        {
            builder.Append(message);
        }

        public override void Visit(ConstraintResult result)
        {
            if(inguard)
                WriteLine($"{result.Constraint.ContextInfo} {result.Constraint.Describe()}");
            else
                WriteLine($"{result.Constraint.ContextInfo} {result.Constraint.Describe()} - actual value was: {result.Token}");
        }

        private void WriteLine(string message)
        {
            if (indent > 0)
                builder.AppendLine(new string(' ', indent) + message);
            else
                builder.AppendLine(message);
        }

        public string Describe(Result result)
        {
            result = result.Optimize();

            builder.Clear();
            result.Accept(this);
            return Message;
        }

        public override void Visit(FieldResult result)
        {
            if (!result.GuardResult.Value || result.ValidationResult.Value)
                return;

            inguard = true;
            result.GuardResult.Accept(this);
            inguard = false;
            result.ValidationResult.Accept(this);
            WriteLine("");
        }

        public override void Visit(Result result)
        {
            WriteLine(result.ToString());
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
                WriteLine($"{result.Rule.ContextInfo} {rule.Alias}");
            }
            base.Visit(result);
        }

        public override void Visit(FuncResult result)
        {
            Write(result.Explain);
        }

        public override void Visit(AndResult result)
        {
            List<Result> results = (inguard ? result.Results : result.Results.Where(r => !r.Value)).ToList();
            if (results.Count == 0)
                return;

            if (results.Count == 1)
            {
                results.First().Accept(this);
                return;
            }
            
            WriteLine("(");
            indent += 2;
            bool first = true;
            if (inguard)
            {
                foreach (Result child in result.Results)
                {
                    if (!first)
                        WriteLine(" AND ");

                    first = false;
                    child.Accept(this);
                }
            }
            else
            {
                foreach (Result child in result.Results.Where(r => !r.Value))
                {
                    if (!first)
                        WriteLine(" AND ");

                    first = false;
                    child.Accept(this);
                }
            }
            indent -= 2;
            WriteLine(")");
        }


        public override void Visit(OrResult result)
        {
            WriteLine("(");
            indent += 2;
            bool first = true;
            foreach (Result child in result.Results)
            {
                if (!first)
                    Write(" OR ");

                first = false;
                child.Accept(this);
            }
            indent -= 2;
            WriteLine(")");
        }

        public override void Visit(NotResult result)
        {
            WriteLine("NOT");
            base.Visit(result);
        }

    }
}
