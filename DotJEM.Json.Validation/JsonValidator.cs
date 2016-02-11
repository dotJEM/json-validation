using System;
using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Rules;
using DotJEM.Json.Validation.Rules.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation
{
    public interface IJsonValidator
    {
        JsonValidatorResult Validate(IJsonValidationContext contenxt, JObject entity);
        JsonValidatorDescription Describe();
    }

    public class JsonValidator : IJsonValidator
    {
        private readonly List<JsonFieldValidator> validators = new List<JsonFieldValidator>();
        protected IGuardConstraintFactory Is { get; } = new ConstraintFactory();
        protected IGuardConstraintFactory Has { get; } = new ConstraintFactory();
        protected IValidatorConstraintFactory Must { get; } = new ValidatorConstraintFactory();
        protected IValidatorConstraintFactory Should { get; } = new ValidatorConstraintFactory();

        protected JsonRule Any => new AnyJsonRule();
        
        protected IJsonValidatorRuleFactory When(JsonRule rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));

            return new JsonValidatorRuleFactory(this, rule);
        }

        protected IJsonValidatorRuleFactory When(string selector, JsonConstraint constraint)
        {
            return When(Field(selector, constraint));
        }

        protected IJsonValidatorRuleFactory When(string selector, string alias, JsonConstraint constraint)
        {
            return When(Field(selector, alias, constraint));
        }

        protected JsonRule Field(string selector, JsonConstraint constraint)
        {
            return Field(selector, selector, constraint);
        }

        protected JsonRule Field(string selector, string alias, JsonConstraint constraint)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (constraint == null) throw new ArgumentNullException(nameof(constraint));
            if (alias == null) throw new ArgumentNullException(nameof(alias));

            return new BasicJsonRule(selector, alias, constraint);
        }

        internal void AddValidator(JsonFieldValidator jsonFieldValidator)
        {
            validators.Add(jsonFieldValidator);
        }

        public virtual JsonValidatorResult Validate(IJsonValidationContext context, JObject entity)
        {
            IEnumerable<JsonRuleResult> results
                = from validator in validators
                  let result = validator.Validate(context, entity)
                  where result != null
                  select result.Optimize();

            return new JsonValidatorResult(results.ToList());
        }
        

        public JsonValidatorDescription Describe()
        {
            IEnumerable<JsonFieldValidatorDescription> descriptions
                = from validator in validators
                  select validator.Describe();

            return new JsonValidatorDescription(this, descriptions.ToList());
        }
    }
}