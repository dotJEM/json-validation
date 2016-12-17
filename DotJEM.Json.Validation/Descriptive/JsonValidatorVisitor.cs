using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Descriptive
{
    public interface IJsonValidatorVisitor
    {
        void Visit(JsonConstraint constraint);
        void Visit(NotJsonConstraint constraint);
        void Visit(AndJsonConstraint constraint);
        void Visit(OrJsonConstraint constraint);


        void Visit(NotJsonRule rule);
        void Visit(AndJsonRule rule);
        void Visit(OrJsonRule rule);
        void Visit(EmbededValidatorRule rule);
        void Visit(AnyJsonRule rule);
        void Visit(FuncJsonRule rule);

        void Visit(JsonFieldValidator validator);
    }

    public class JsonValidatorVisitor
    {

    }
}
