using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public abstract class CompositeJsonRule : JsonRule
    {
        private readonly List<JsonRule> rules;

        public IEnumerable<JsonRule> Rules => rules;

        protected CompositeJsonRule(params JsonRule[] rules)
        {
            this.rules = rules.ToList();
        }

        protected JsonRule OptimizeAs<TRule>() where TRule : CompositeJsonRule, new()
        {
            if (rules.Count == 1)
                return rules[0];

            return rules
                .Select(c => c.Optimize())
                .Aggregate(new TRule(), (c, next) =>
                {
                    TRule and = next as TRule;
                    if (and != null)
                    {
                        c.rules.AddRange(and.Rules);
                    }
                    else
                    {
                        c.rules.Add(next);
                    }
                    return c;
                });
        }
    }
}