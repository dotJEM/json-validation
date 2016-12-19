using System;
using DotJEM.Json.Validation.Results;

namespace DotJEM.Json.Validation.Visitors
{
    public interface IJsonResultVisitor :
        IJsonResultVisitor<AbstractResult>,
        IJsonResultVisitor<Result>,
        IJsonResultVisitor<CompositeResult>,
        IJsonResultVisitor<AndResult>,
        IJsonResultVisitor<OrResult>,
        IJsonResultVisitor<NotResult>,
        IJsonResultVisitor<AnyResult>,
        IJsonResultVisitor<ConstraintResult>,
        IJsonResultVisitor<RuleResult>,
        IJsonResultVisitor<ValidatorResult>,
        IJsonResultVisitor<EmbededValidatorResult>
    {
    }

    public interface IJsonResultVisitor<in TResult> where TResult : AbstractResult
    {
        void Visit(TResult result);
    }

    public abstract class JsonResultVisitor : IJsonResultVisitor
    {
        public virtual void Visit(AbstractResult result)
        {
            throw new NotImplementedException($"No approriate visitor methods was found for type: {result.GetType()}.");
        }

        public virtual void Visit(Result result)
        {
            Visit((AbstractResult)result);
        }

        public virtual void Visit(CompositeResult result)
        {
            foreach (AbstractResult child in result.Results)
                child.Accept(this);
        }

        public virtual void Visit(AndResult result)
        {
            foreach (AbstractResult child in result.Results)
                child.Accept(this);
        }

        public virtual void Visit(OrResult result)
        {
            foreach (AbstractResult child in result.Results)
                child.Accept(this);
        }

        public virtual void Visit(NotResult result)
        {
            result.Result.Accept(this);
        }

        public virtual void Visit(AnyResult result)
        {
            Visit((AbstractResult)result);
        }

        public virtual void Visit(ConstraintResult result)
        {
            Visit((AbstractResult)result);
        }

        public virtual void Visit(RuleResult result)
        {
            result.Result.Accept(this);
        }

        public virtual void Visit(ValidatorResult result)
        {
            foreach (AbstractResult child in result.Results)
                child.Accept(this);
        }

        public virtual void Visit(EmbededValidatorResult result)
        {
            result.Result.Accept(this);
        }
    }

    public static class JsonResultVisitorExtensions
    {
        public static TVisitor Accept<TVisitor>(this AbstractResult result, TVisitor visitor) where TVisitor : IJsonResultVisitor
        {
            //Note: Dynamic dispatch forcing runtime resolution of the correct overload.
            visitor.Visit((dynamic)result);
            return visitor;
        }
    }
}