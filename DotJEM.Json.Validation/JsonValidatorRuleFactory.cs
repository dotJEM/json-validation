using System;
using System.Reflection.Emit;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Rules;
using DotJEM.Json.Validation.Selectors;

namespace DotJEM.Json.Validation
{
    public interface IJsonValidatorRuleFactory
    {
        void Then(JsonRule validator);
        void Then(FieldSelector selector, CapturedConstraint validator);
        void Then(FieldSelector selector, string alias, CapturedConstraint validator);
        void Then(ISelfReferencingRule @ref, CapturedConstraint constraint);

        IForFieldSelector Use<TValidator>() where TValidator : JsonValidator, new();
        IForFieldSelector Use<TValidator>(TValidator instance) where TValidator : JsonValidator;
        IForFieldSelector Use(Type validatorType);
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

        public void Then(FieldSelector selector, CapturedConstraint constraint) => Then(selector, selector.Path, constraint);

        public void Then(FieldSelector selector, string alias, CapturedConstraint constraint) => Then(new BasicJsonRule(selector, alias, constraint));

        public void Then(ISelfReferencingRule @ref, CapturedConstraint constraint)
        {
            try
            {
                CollectSingleSelectorVisitor visitor = rule.Accept(new CollectSingleSelectorVisitor());
                Then(visitor.SelectorPath, visitor.Alias, constraint);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("A self referencing rule (It) can only be used when a single field is used in the When clause.", ex);
            }
        }
        public void Then(JsonRule rule)
        {
            //Note: Captured Rule.
            //rule.ContextInfo = "Then";
            validator.AddValidator(new JsonFieldValidator(this.rule, rule));
        }

        public IForFieldSelector Use<TValidator>() where TValidator : JsonValidator, new()
        {
            return new ForFieldSelector(this, rule, new TValidator()); 
        }

        public IForFieldSelector Use<TValidator>(TValidator instance) where TValidator : JsonValidator
        {
            return new ForFieldSelector(this, rule, instance);
        }

        public IForFieldSelector Use(Type validatorType)
        {
            if(!validatorType.IsSubclassOf(typeof(JsonValidator)))
                throw new ArgumentException("The given type must inherit from JsonValidator");

            return Use((JsonValidator)Activator.CreateInstance(validatorType));
        }
    }

    public interface IForFieldSelector
    {
        void For(FieldSelector selector, string alias = null);
        void For(ISelfReferencingRule selector, string alias = null);
    }

    public class ForFieldSelector : IForFieldSelector
    {
        private readonly JsonRule rule;
        private readonly JsonValidator validator;
        private readonly JsonValidatorRuleFactory factory;


        public ForFieldSelector(JsonValidatorRuleFactory factory, JsonRule rule, JsonValidator validator)
        {
            this.factory = factory;
            this.rule = rule;
            this.validator = validator;
        }

        public void For(FieldSelector selector, string alias = null)
        {
            factory.Then(new EmbededValidatorRule(selector, alias ?? selector.Path, validator));
        }

        public void For(ISelfReferencingRule selector, string alias = null)
        {
            try
            {
                CollectSingleSelectorVisitor visitor = rule.Accept(new CollectSingleSelectorVisitor());
                For(visitor.SelectorPath, alias);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("A self referencing rule (It) can only be used when a single field is used in the When clause.", ex);
            }
        }
    }

    public interface ISelfReferencingRule
    {
    }

    public class SelfReferencingRule : ISelfReferencingRule
    {
    }
}