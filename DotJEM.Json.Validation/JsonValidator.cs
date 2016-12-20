using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Constraints.Common;
using DotJEM.Json.Validation.Constraints.String;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Rules;
using DotJEM.Json.Validation.Selectors;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation
{
    public interface IJsonValidator
    {
        ValidatorResult Validate(JObject entity, IJsonValidationContext contenxt);
    }

    public class JsonValidator : IJsonValidator
    {
        private readonly List<JsonFieldValidator> validators = new List<JsonFieldValidator>();

        protected IGuardConstraintFactory Is { get; } = new ConstraintFactory(null, "Is");
        protected IGuardConstraintFactory Has { get; } = new ConstraintFactory(null, "Has");
        protected IValidatorConstraintFactory Must { get; } = new ValidatorConstraintFactory(null, "Must");
        protected IValidatorConstraintFactory Should { get; } = new ValidatorConstraintFactory(null, "Should");

        protected ISelfReferencingRule It { get; } = new SelfReferencingRule();

        protected JsonRule Any => new AnyJsonRule();
        
        protected IJsonValidatorRuleFactory When(JsonRule rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            //Note: Captured Rule.
            rule.RuleContext = "When";
            return new JsonValidatorRuleFactory(this, rule);
        }

        //TODO: There is little reason that these should not be callable from the outside of the validator, so we can make them public at some point. 
        //      (We might wan't separate interfaces for building the validator and using it)
        //       - When this happens, many of these WHEN and FIELD methods can actually be extensions (Can't remember if that forces "this.", if so we don't wan't that).
        protected IJsonValidatorRuleFactory When(Func<JObject, bool> constraintFunc, string explain)
        {
            return When(new FuncJsonRule(constraintFunc, explain));
        }

        protected IJsonValidatorRuleFactory When(FieldSelector selector, Func<JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, Is.Matching(constraintFunc, explain)));
        }

        protected IJsonValidatorRuleFactory When(FieldSelector selector, string alias, Func<JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, alias, Is.Matching(constraintFunc, explain)));
        }

        protected IJsonValidatorRuleFactory When(FieldSelector selector, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, Is.Matching(constraintFunc, explain)));
        }

        protected IJsonValidatorRuleFactory When(FieldSelector selector, string alias, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, alias, Is.Matching(constraintFunc, explain)));
        }

        protected IJsonValidatorRuleFactory When(FieldSelector selector, CapturedConstraint captured)
        {
            return When(Field(selector, captured));
        }

        protected IJsonValidatorRuleFactory When(FieldSelector selector, string alias, CapturedConstraint captured)
        {
            return When(Field(selector, alias, captured));
        }

        protected JsonRule Field(FieldSelector selector, CapturedConstraint captured)
        {
            return Field(selector, selector.Path, captured);
        }

        protected JsonRule Field(FieldSelector selector, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return Field(selector, selector.Path, Is.Matching(constraintFunc, explain));
        }

        protected JsonRule Field(FieldSelector selector, string alias, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return Field(selector, alias, Is.Matching(constraintFunc, explain));
        }

        protected JsonRule Field(FieldSelector selector, string alias, CapturedConstraint captured)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (captured == null) throw new ArgumentNullException(nameof(captured));
            if (alias == null) throw new ArgumentNullException(nameof(alias));

            return new BasicJsonRule(selector, alias, captured);
        }

        protected IForFieldSelector Use<TValidator>() where TValidator : JsonValidator, new()
        {
            return When(Any).Use<TValidator>();
        }

        protected IForFieldSelector Use<TValidator>(TValidator instance) where TValidator : JsonValidator
        {
            return When(Any).Use(instance);
        }

        protected IForFieldSelector Use(Type validatorType)
        {
            return When(Any).Use(validatorType);
        }

        public void AddValidator(JsonFieldValidator jsonFieldValidator)
        {
            validators.Add(jsonFieldValidator);
        }

        public virtual ValidatorResult Validate(JObject entity, IJsonValidationContext context)
        {
            IEnumerable<Result> results
                = from validator in validators
                  let result = validator.Validate(entity, context)
                  where result != null
                  select result.Optimize();

            return new ValidatorResult(this, results.ToList());
        }
    }

}