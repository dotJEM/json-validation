using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public sealed class NotJsonRule : JsonRule
    {
        public JsonRule Rule { get; }

        public NotJsonRule(JsonRule rule)
        {
            Rule = rule;
        }

        public override AbstractResult Test(IJsonValidationContext contenxt, JObject entity)
        {
            return !Rule.Test(contenxt, entity);
        }

        public override JsonRule Optimize()
        {
            NotJsonRule not = Rule as NotJsonRule;
            return not != null ? not.Rule : base.Optimize();
        }

        //public override Description Describe()
        //{
        //    return new JsonNotRuleDescription(Rule.Describe());
        //}
    }
}