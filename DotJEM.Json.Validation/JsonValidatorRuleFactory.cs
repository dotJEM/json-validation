using System;
using System.Linq;
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
    }

    public class CollectSingleSelectorVisitor : JsonRuleVisitor, IJsonRuleVisitor<CompositeJsonRule>, IJsonRuleVisitor<BasicJsonRule>, IJsonRuleVisitor<NotJsonRule>, IJsonRuleVisitor<FuncJsonRule>
    {
        private bool root = true;
        public string Selector { get; private set; }
        public string Alias { get; private set; }

        public IJsonRuleVisitor Visit(CompositeJsonRule rule)
        {
            root = false;
            return rule.Rules.Aggregate(this, (visitor, r) => r.Accept(visitor));
        }

        public IJsonRuleVisitor Visit(NotJsonRule rule)
        {
            return rule.Rule.Accept(this);
        }

        public IJsonRuleVisitor Visit(FuncJsonRule rule)
        {
            //TODO : Support for using lambdas to select values.
            //if(!root)
                throw new InvalidOperationException("Json Rule Tree had multiple different selectors.");
            //return this;
        }

        public IJsonRuleVisitor Visit(BasicJsonRule rule)
        {
            if (Selector == null)
            {
                Selector = rule.Selector;
                Alias = rule.Alias;
                return this;
            }

            if (Selector != rule.Selector)
                throw new InvalidOperationException("Json Rule Tree had multiple different selectors.");

            if (Alias == null)
            {
                //Note: If Alias is null, we allow the next rule to provide one.
                Alias = rule.Alias;
                return this;
            }

            if (Alias != rule.Alias)
            {
                //Note: If multiple aliases was found, we defer back to the selector.
                Alias = Selector;
            }

            return this;
        }
    }
}