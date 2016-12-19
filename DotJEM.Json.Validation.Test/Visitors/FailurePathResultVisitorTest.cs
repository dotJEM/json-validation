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
            AbstractResult result = new Result(true) & new Result(true) | new Result(true) & !new Result(false);

            //var visitor = result.Accept(new DescribeFailurePathVisitor());
            var visitor = new DescribeFailurePathVisitor();
            string description = visitor.Describe(result);
            //Assert.That(description, Is.EqualTo("No Errors."));
        }
    }

    public class DescribeFailurePathVisitor : JsonResultVisitor
    {
        public string Message { get; private set; }

        public string Describe(AbstractResult result)
        {
            Message = "";
            result.Accept(this);
            return Message;
        }

        public override void Visit(AbstractResult result)
        {
            Message += result.ToString();
        }

        public override void Visit(AndResult result)
        {
            Message += "(" + NewLine;

            bool first = true;
            foreach (AbstractResult child in result.Results)
            {
                if (!first)
                    Message += " AND ";

                first = false;
                child.Accept(this);
            }

            Message += ")" + NewLine;
        }


        public override void Visit(OrResult result)
        {
            Message += "(" + NewLine;

            bool first = true;
            foreach (AbstractResult child in result.Results)
            {
                if (!first)
                    Message += " OR ";

                first = false;
                child.Accept(this);
            }

            Message += ")" + NewLine;
        }

        public override void Visit(NotResult result)
        {
            Message += " ! ";
            base.Visit(result);
        }

    }
}
