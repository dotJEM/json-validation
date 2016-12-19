using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Selectors;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public sealed class BasicJsonRule : JsonRule
    {
        public FieldSelector Selector { get; }
        public string Alias { get; }

        private readonly JsonConstraint constraint;

        public BasicJsonRule(FieldSelector selector, string alias, CapturedConstraint constraint)
        {
            this.Selector = selector;
            this.Alias = alias;
            this.constraint = constraint.Constraint.Optimize();
        }

        public override AbstractResult Test(JObject entity, IJsonValidationContext context)
        {
            return new AndResult(
                (from token in Selector.SelectTokens(entity)
                 select (AbstractResult)new RuleResult(this, constraint.DoMatch(token, context))).ToList());
        }
    }

    public sealed class EmbededValidatorRule : JsonRule
    {
        public FieldSelector Selector { get; }
        public string Alias { get; }

        private readonly JsonValidator validator;

        public EmbededValidatorRule(FieldSelector selector, string alias, JsonValidator validator)
        {
            Selector = selector;
            Alias = alias;
            this.validator = validator;
        }

        public override AbstractResult Test(JObject entity, IJsonValidationContext context)
        {
            return new AndResult(
                (from token in Selector.SelectTokens(entity)
                 select (AbstractResult)new EmbededValidatorResult(this, validator.Validate((JObject)token, context))).ToList());
        }
    }

    public sealed class AnyJsonRule : JsonRule
    {
        public override AbstractResult Test(JObject entity, IJsonValidationContext contenxt)
        {
            return new RuleResult(this, new AnyResult());
        }
    }

    public sealed class FuncJsonRule : JsonRule
    {
        private readonly string explain;
        private readonly Func<JObject, bool> func;

        public FuncJsonRule(Func<JObject, bool> func, string explain)
        {
            this.func = func;
            this.explain = explain;
        }

        public override AbstractResult Test(JObject entity, IJsonValidationContext contenxt)
        {
            return new RuleResult(this, new Result(func(entity)));
        }
    }
}