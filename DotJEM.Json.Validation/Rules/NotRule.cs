using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public sealed class NotRule : Rule
    {
        public Rule Rule { get; }

        public NotRule(Rule rule)
        {
            Rule = rule;
        }

        public override Result Test(JObject entity, IJsonValidationContext contenxt)
        {
            return !Rule.Test(entity, contenxt);
        }

        public override Rule Optimize()
        {
            NotRule not = Rule as NotRule;
            return not != null ? not.Rule.Optimize() : new NotRule(Rule.Optimize());
        }
    }
}