using System;
using System.Collections.Generic;
using DotJEM.Json.Validation.Results;

namespace DotJEM.Json.Validation.Visitors
{
    public abstract class ResultVisitor : IResultVisitor
    {
        public virtual void Visit(Result visitee)
        {
            throw new NotImplementedException($"No approriate visitor methods was found for type: {visitee.GetType()}.");
        }

        public virtual void Visit(FieldResult visitee)
        {
            visitee.GuardResult.Accept(this);
            visitee.ValidationResult.Accept(this);
        }

        public virtual void Visit(ValidatorResult visitee) => AcceptAll(visitee.Results);
        public virtual void Visit(CompositeResult visitee) => AcceptAll(visitee.Results);
        public virtual void Visit(AndResult visitee) => Visit((CompositeResult)visitee);
        public virtual void Visit(OrResult visitee) => Visit((CompositeResult)visitee);


        public virtual void Visit(FuncResult visitee) => Visit((Result)visitee);
        public virtual void Visit(AnyResult visitee) => Visit((Result)visitee);
        public virtual void Visit(SkippedResult visitee) => Visit((Result)visitee);
        public virtual void Visit(ConstraintResult visitee) => Visit((Result)visitee);

        public virtual void Visit(RuleResult visitee) => visitee.Result.Accept(this);
        public virtual void Visit(NotResult visitee) => visitee.Result.Accept(this);
        public virtual void Visit(EmbededValidatorResult visitee) => visitee.Result.Accept(this);

        private void AcceptAll(IEnumerable<Result> results)
        {
            foreach (Result child in results)
                child.Accept(this);
        }
    }
}