using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
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

        public IEnumerable<JsonFieldValidator> Validators => validators;

        protected IGuardConstraintFactory Is { get; } = new ConstraintFactory(null, "is");
        protected IGuardConstraintFactory Has { get; } = new ConstraintFactory(null, "has");
        protected IValidatorConstraintFactory Must { get; } = new ValidatorConstraintFactory(null, "must");
        protected IValidatorConstraintFactory Should { get; } = new ValidatorConstraintFactory(null, "should");

        protected ISelfReferencingRule It { get; } = new SelfReferencingRule();

        protected IBeConstraintFactory Be { get; } = new ConstraintFactory(null, "be");
        protected IHaveConstraintFactory Have { get; } = new ConstraintFactory(null, "have");

        protected Rule Any => new AnyRule();

        #region When

        public IJsonValidatorRuleFactory When(Rule rule)
        {
            if (rule == null) throw new ArgumentNullException(nameof(rule));
            //Note: Captured Rule.
            //rule.ContextInfo = "When";
            return new JsonValidatorRuleFactory(this, rule);
        }

        //TODO: There is little reason that these should not be callable from the outside of the validator, so we can make them public at some point. 
        //      (We might wan't separate interfaces for building the validator and using it)
        //       - When this happens, many of these WHEN and FIELD methods can actually be extensions (Can't remember if that forces "this.", if so we don't wan't that).
        public IJsonValidatorRuleFactory When(Func<JObject, bool> constraintFunc, string explain)
        {
            return When(new FuncRule(constraintFunc, explain));
        }

        public IJsonValidatorRuleFactory When(FieldSelector selector, Func<JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, Is.Matching(constraintFunc, explain)));
        }

        public IJsonValidatorRuleFactory When(FieldSelector selector, string alias, Func<JToken, bool> constraintFunc, string explain)
        {
            return When(Field(new AliasedFieldSelector(alias, selector), Is.Matching(constraintFunc, explain)));
        }

        public IJsonValidatorRuleFactory When(FieldSelector selector, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return When(Field(selector, Is.Matching(constraintFunc, explain)));
        }

        public IJsonValidatorRuleFactory When(FieldSelector selector, string alias, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return When(Field(new AliasedFieldSelector(alias, selector), Is.Matching(constraintFunc, explain)));
        }

        public IJsonValidatorRuleFactory When(FieldSelector selector, CapturedConstraint captured)
        {
            return When(Field(selector, captured));
        }

        public IJsonValidatorRuleFactory When(FieldSelector selector, string alias, CapturedConstraint captured)
        {
            return When(Field(new AliasedFieldSelector(alias, selector), captured));
        } 

        #endregion

        #region Field

        public Rule Field(FieldSelector selector, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return Field(selector, Is.Matching(constraintFunc, explain));
        }

        public Rule Field(FieldSelector selector, string alias, Func<IJsonValidationContext, JToken, bool> constraintFunc, string explain)
        {
            return Field(selector, alias, Is.Matching(constraintFunc, explain));
        }

        public Rule Field(FieldSelector selector, string alias, CapturedConstraint captured)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (captured == null) throw new ArgumentNullException(nameof(captured));
            if (alias == null) throw new ArgumentNullException(nameof(alias));

            return Field(new AliasedFieldSelector(alias, selector), captured);
        }

        public Rule Field(FieldSelector selector, CapturedConstraint captured)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));
            if (captured == null) throw new ArgumentNullException(nameof(captured));

            return new BasicRule(selector, captured);
        } 
        
        #endregion

        #region Use

        public IForFieldSelector Use<TValidator>() where TValidator : JsonValidator, new()
        {
            return When(Any).Use<TValidator>();
        }

        public IForFieldSelector Use<TValidator>(TValidator instance) where TValidator : JsonValidator
        {
            return When(Any).Use(instance);
        }

        public IForFieldSelector Use(Type validatorType)
        {
            return When(Any).Use(validatorType);
        } 

        #endregion

        public CapturedConstraint ComparedTo(FieldSelector selector, Func<JToken, CapturedConstraint> factory)
        {
            return new CapturedConstraint(new LazyConstraint(selector, factory), "compared to");
        }

        public CapturedConstraint ComparedTo(FieldSelector selector, string alias, Func<JToken, CapturedConstraint> factory) 
            => ComparedTo(new AliasedFieldSelector(alias, selector), factory);

        public CapturedConstraint CompareTo(FieldSelector selector, Func<JToken, CapturedConstraint> factory)
            => ComparedTo(selector, factory);

        public CapturedConstraint CompareTo(FieldSelector selector, string alias, Func<JToken, CapturedConstraint> factory)
            => ComparedTo(selector, alias, factory);

        public virtual ValidatorResult Validate(JObject entity, IJsonValidationContext context)
        {
            context = new DynamicContext(context, entity);

            IEnumerable<Result> results
                = from validator in Validators
                  let result = validator.Validate(entity, context)
                  where result != null
                  select result.Optimize();

            return new ValidatorResult(this, results.ToList());
        }

        public void AddValidator(JsonFieldValidator jsonFieldValidator)
        {
            if (jsonFieldValidator == null)
                throw new ArgumentNullException(nameof(jsonFieldValidator));

            validators.Add(jsonFieldValidator);
        }
    }
}