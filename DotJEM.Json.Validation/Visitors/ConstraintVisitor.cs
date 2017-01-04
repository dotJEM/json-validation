using System;
using DotJEM.Json.Validation.Constraints;

namespace DotJEM.Json.Validation.Visitors
{
    public abstract class ConstraintVisitor : IConstraintVisitor
    {
        public void Visit(JsonConstraint visitee)
        {
            throw new NotImplementedException($"No approriate visitor methods was found for type: {visitee.GetType()}.");
        }

        public void Visit(CompositeJsonConstraint visitee)
        {
            foreach (JsonConstraint constraint in visitee.Constraints)
                constraint.Accept(this);
        }

        public void Visit(NotJsonConstraint visitee) => visitee.Constraint.Accept(this);
        public void Visit(AndJsonConstraint visitee) => Visit((CompositeJsonConstraint) visitee);
        public void Visit(OrJsonConstraint visitee) => Visit((CompositeJsonConstraint)visitee);
    }
}