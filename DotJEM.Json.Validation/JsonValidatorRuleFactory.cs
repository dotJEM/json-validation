using System;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation
{
    public interface IJsonValidatorRuleFactory
    {
        void Then(JsonRule validator);
        void Then(string selector, CapturedConstraint validator);
        void Then(ISelfReferencingRule @ref, CapturedConstraint constraint);

        IForFieldSelector Use<TValidator>() where TValidator : JsonValidator;
    }

    public interface IForFieldSelector
    {
        void For(string selector);
        void For(ISelfReferencingRule selector);
    }

    public class JsonValidatorRuleFactory : IJsonValidatorRuleFactory
    {
        private readonly JsonRule rule;
        private readonly JsonValidator validator;

        public JsonValidatorRuleFactory(JsonValidator validator, JsonRule rule)
        {
            this.validator = validator;
            this.rule = rule;
        }

        public void Then(JsonRule rule)
        {
            //Note: Captured Rule.
            rule.RuleContext = "Then";
            validator.AddValidator(new JsonFieldValidator(this.rule, rule));
        }

        public void Then(string selector, CapturedConstraint constraint) => Then(selector, selector, constraint);

        public void Then(string selector, string alias, CapturedConstraint constraint) => Then(new BasicJsonRule(selector, alias, constraint));

        public void Then(ISelfReferencingRule @ref, CapturedConstraint constraint)
        {
            try
            {
                CollectSingleSelectorVisitor visitor = rule.Accept(new CollectSingleSelectorVisitor());
                Then(visitor.Selector, visitor.Alias, constraint);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("A self referencing rule (It) can only be used when a single field is used in the When clause.", ex);
            }
        }

        public IForFieldSelector Use<TValidator>() where TValidator : JsonValidator
        {
            throw new NotImplementedException();
        }
    }
    public interface ISelfReferencingRule
    {
    }

    public class SelfReferencingRule : ISelfReferencingRule
    {
    }
}