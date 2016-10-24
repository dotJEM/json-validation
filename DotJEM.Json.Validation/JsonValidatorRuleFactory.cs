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
            BasicJsonRule basic = rule as BasicJsonRule;
            if (basic == null)
                throw new InvalidOperationException("A self referencing rule (It) can only be used when a single field is used in the When clause.");

            Then(basic.Selector, basic.Alias, constraint);
        }
    }
}