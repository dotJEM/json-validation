using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [DebuggerDisplay("{Selector}  {Constraint}")]
    public sealed class BasicRule : Rule
    {
        public FieldSelector Selector { get; }
        public string Alias { get; }

        public JsonConstraint Constraint { get; }

        public BasicRule(FieldSelector selector, string alias, CapturedConstraint constraint)
        {
            this.Selector = selector;
            this.Alias = alias;
            this.Constraint = constraint.Constraint.Optimize();
        }

        public override Result Test(JObject entity, IJsonValidationContext context)
        {
            JToken[] tokens = Selector.SelectTokens(entity).ToArray();
            return new AndResult(
                (from token in tokens
                 select (Result)new RuleResult(this, Constraint.DoMatch(token, context))).ToList());
        }
    }

    public sealed class EmbededValidatorRule : Rule
    {
        public FieldSelector Selector { get; }
        public string Alias { get; }

        public JsonValidator Validator { get; }

        public EmbededValidatorRule(FieldSelector selector, string alias, JsonValidator validator)
        {
            Selector = selector;
            Alias = alias;
            this.Validator = validator;
        }

        public override Result Test(JObject entity, IJsonValidationContext context)
        {
            JToken[] tokens = Selector.SelectTokens(entity).ToArray();
            return new AndResult(
                (from token in tokens
                 select (Result)new EmbededValidatorResult(this, Validator.Validate((JObject)token, context))).ToList());
        }
    }

    public sealed class AnyRule : Rule
    {
        public override Result Test(JObject entity, IJsonValidationContext contenxt)
        {
            return new RuleResult(this, new AnyResult());
        }
    }

    public sealed class FuncRule : Rule
    {
        private readonly string explain;
        private readonly Func<JObject, bool> func;

        public FuncRule(Func<JObject, bool> func, string explain)
        {
            this.func = func;
            this.explain = explain;
        }

        public override Result Test(JObject entity, IJsonValidationContext contenxt)
        {
            return new RuleResult(this, new FuncResult(func(entity), explain));
        }
    }
}