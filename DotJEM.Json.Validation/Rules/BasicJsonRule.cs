using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Rules.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public sealed class BasicJsonRule : JsonRule
    {
        private static readonly Regex arraySelector = new Regex(@".*\[\*|.+].*", RegexOptions.Compiled);

        private readonly string selector;
        private readonly string alias;

        private readonly JsonConstraint constraint;
        private readonly bool hasArray;

        public BasicJsonRule(string selector, string alias, JsonConstraint constraint)
        {
            this.selector = selector;
            this.alias = alias;
            this.constraint = constraint.Optimize();
            this.hasArray = arraySelector.IsMatch(selector);
        }

        public override JsonRuleResult Test(IJsonValidationContext context, JObject entity)
        {
            return new AndJsonRuleResult(
                (from token in SelectTokens(entity)
                 select (JsonRuleResult)new BasicJsonRuleResult(selector, token?.Path, constraint.DoMatch(context, token))).ToList());
        }

        private IEnumerable<JToken> SelectTokens(JObject entity)
        {
            if (hasArray)
            {
                return entity.SelectTokens(selector);
            }
            return new[] { entity.SelectToken(selector) };
        }

        public override Description Describe()
        {
            return new BasicJsonRuleDescription(alias, selector, constraint);
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
}