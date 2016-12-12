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
        protected IGuardConstraintFactory Is { get; } = new ConstraintFactory(null, "Is");
        protected IGuardConstraintFactory Has { get; } = new ConstraintFactory(null, "Has");
        protected IValidatorConstraintFactory Must { get; } = new ValidatorConstraintFactory(null, "Must");
        protected IValidatorConstraintFactory Should { get; } = new ValidatorConstraintFactory(null, "Should");

        protected ISelfReferencingRule It { get; } = new SelfReferencingRule();

        protected JsonRule Any => new AnyJsonRule();
        
        protected IJsonValidatorRuleFactory When(JsonRule rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            rule.RuleContext = "When";
            return new JsonValidatorRuleFactory(this, rule);
        }

        

        protected IJsonValidatorRuleFactory When(Func<JObject, bool> constraintFunc, string explain)
        {
            return When(new FuncJsonRule(constraintFunc, explain));
        }

        protected IJsonValidatorRuleFactory When(string selector, Func<JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, Is.Matching(constraintFunc, explain)));
        }

        protected IJsonValidatorRuleFactory When(string selector, string alias, Func<JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, alias, Is.Matching(constraintFunc, explain)));
        }

        protected IJsonValidatorRuleFactory When(string selector, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, Is.Matching(constraintFunc, explain)));
        }

        protected IJsonValidatorRuleFactory When(string selector, string alias, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, alias, Is.Matching(constraintFunc, explain)));
        }

        protected IJsonValidatorRuleFactory When(string selector, CapturedConstraint captured)
        {
            return When(Field(selector, captured));
        }

        protected IJsonValidatorRuleFactory When(string selector, string alias, CapturedConstraint captured)
        {
            return When(Field(selector, alias, captured));
        }

        protected JsonRule Field(string selector, CapturedConstraint captured)
        {
            return Field(selector, selector, captured);
        }

        protected JsonRule Field(string selector, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return Field(selector, selector, Is.Matching(constraintFunc, explain));
        }

        protected JsonRule Field(string selector, string alias, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return Field(selector, alias, Is.Matching(constraintFunc, explain));
        }

        protected JsonRule Field(string selector, string alias, CapturedConstraint captured)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (captured == null) throw new ArgumentNullException(nameof(captured));
            if (alias == null) throw new ArgumentNullException(nameof(alias));

            return new BasicJsonRule(selector, alias, captured);
        }

        internal void AddValidator(JsonFieldValidator jsonFieldValidator)
        {
            validators.Add(jsonFieldValidator);
        }

        public virtual JsonValidatorResult Validate(IJsonValidationContext context, JObject entity)
        {
            IEnumerable<AbstractResult> results
                = from validator in validators
                  let result = validator.Validate(context, entity)
                  where result != null
                  select result.Optimize();

            return new JsonValidatorResult(results.ToList());
        }
        
        public JsonValidatorDescription Describe()
        {
            //IEnumerable<JsonFieldValidatorDescription> descriptions
            //    = from validator in validators
            //      select validator.Describe();

            //return new JsonValidatorDescription(this, descriptions.ToList());
            return null;
        }
    }
}