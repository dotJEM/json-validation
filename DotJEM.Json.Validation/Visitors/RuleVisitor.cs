using System;
using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Visitors;

public abstract class RuleVisitor : IRuleVisitor
{
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
        foreach (JsonFieldValidator validator in visitee.Validator.Validators)
        {
            validator.Guard.Accept(this);
            validator.Guard.Accept(this);
        }
    }

    public virtual void Visit(AnyRule visitee) => Visit((Rule)visitee);
    public virtual void Visit(FuncRule visitee) => Visit((Rule)visitee);
    public virtual void Visit(BasicRule visitee) => Visit((Rule)visitee);
}