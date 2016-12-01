using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Rules.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public sealed class BasicJsonRule : JsonRule
    {
        private static readonly Regex arraySelector = new Regex(@".*\[\*|.+].*", RegexOptions.Compiled);

        public string Selector { get; }
        public string Alias { get; }

        private readonly JsonConstraint constraint;
        private readonly bool hasArray;

        public BasicJsonRule(string selector, string alias, CapturedConstraint constraint)
        {
            this.Selector = selector;
            this.Alias = alias;
            this.constraint = constraint.Constraint.Optimize();
            this.hasArray = arraySelector.IsMatch(selector);
        }

        public override JsonRuleResult Test(IJsonValidationContext context, JObject entity)
        {
            return new AndJsonRuleResult(
                (from token in SelectTokens(entity)
                 select (JsonRuleResult)new BasicJsonRuleResult(Selector, token, constraint.DoMatch(context, token))).ToList());
        }

        private IEnumerable<JToken> SelectTokens(JObject entity)
        {
            if (hasArray)
            {
                return entity.SelectTokens(Selector);
            }
            return new[] { entity.SelectToken(Selector) };
        }

        public override Description Describe()
        {
            return new BasicJsonRuleDescription(Alias, Selector, constraint);
        }
    }

    public sealed class AnyJsonRule : JsonRule
    {
        public override JsonRuleResult Test(IJsonValidationContext contenxt, JObject entity)
        {
            return new AnyJsonRuleResult();
        }

        public override Description Describe()
        {
            return new AnyJsonRuleDescription();
        }
    }

    public sealed class FuncJsonRule : JsonRule
    {
        private readonly Func<JObject, bool> func;
        private readonly string explain;

        public FuncJsonRule(Func<JObject, bool> func, string explain)
        {
            this.func = func;
            this.explain = explain;
        }

        public override JsonRuleResult Test(IJsonValidationContext contenxt, JObject entity)
        {
            return new FuncJsonRuleResult(func, func(entity));
        }

        public override Description Describe()
        {
            throw new System.NotImplementedException();
        }
    }
}