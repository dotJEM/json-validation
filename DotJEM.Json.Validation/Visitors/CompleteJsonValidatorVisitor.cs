using System;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Visitors;

public abstract class CompleteJsonValidatorVisitor : ICompleteJsonValidatorVisitor
{
    public void Visit(JsonFieldValidator visitee)
    {
        visitee.Guard.Accept(this);
        visitee.Rule.Accept(this);
    }

    public void Visit(JsonValidator visitee)
    {
        foreach (JsonFieldValidator validator in visitee.Validators)
            validator.Accept(this);
    }

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
    public void Visit(AndJsonConstraint visitee) => Visit((CompositeJsonConstraint)visitee);
    public void Visit(OrJsonConstraint visitee) => Visit((CompositeJsonConstraint)visitee);

    public virtual void Visit(Rule visitee)
    {
        throw new NotImplementedException($"No approriate visitor methods was found for type: {visitee.GetType()}.");
    }

    public virtual void Visit(NotRule visitee) => visitee.Rule.Accept(this);

    public virtual void Visit(CompositeRule visitee)
    {
        foreach (Rule child in visitee.Rules)
            child.Accept(this);
    }

    public virtual void Visit(AndRule visitee) => Visit((CompositeRule)visitee);
    public virtual void Visit(OrRule visitee) => Visit((CompositeRule)visitee);

    public virtual void Visit(EmbededValidatorRule visitee)
    {
        visitee.Validator.Accept(this);
    }

    public virtual void Visit(AnyRule visitee) => Visit((Rule)visitee);
    public virtual void Visit(FuncRule visitee) => Visit((Rule)visitee);

    public virtual void Visit(BasicRule visitee) => visitee.Constraint.Accept(this);
}