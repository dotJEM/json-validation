using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Visitors;
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
            Result result = new AnyResult() & new AnyResult() | new AnyResult() & !new FuncResult(false, "No");

            //var visitor = result.Accept(new DescribeFailurePathVisitor());
            var visitor = new DescribeFailurePathVisitor();
            string description = visitor.Describe(result);
            Console.WriteLine(description);
            //Assert.That(description, Is.EqualTo("No Errors."));
        }
    }

    public class DescribeFailurePathVisitor : JsonResultVisitor
    {
        private int indent = 0;
        private readonly StringBuilder builder = new StringBuilder();
        public string Message => builder.ToString();


        private void Write(string message)
        {
            builder.Append(message);
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
            builder.Clear();
            result.Accept(this);
            return Message;
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
            WriteLine(" " + result.Rule.RuleContext + " ");
            base.Visit(result);
        }

        public override void Visit(FuncResult result)
        {
            Write(result.Explain);
        }

        public override void Visit(AndResult result)
        {
            WriteLine("(");
            indent += 2;
            bool first = true;
            foreach (Result child in result.Results)
            {
                if (!first)
                    WriteLine(" AND ");

                first = false;
                child.Accept(this);
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
