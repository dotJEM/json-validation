using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public abstract class CompositeRule : Rule
    {
        private readonly List<Rule> rules;

        public IEnumerable<Rule> Rules => rules;

        protected CompositeRule(params Rule[] rules)
        {
            this.rules = rules.ToList();
        }

        protected Rule OptimizeAs<TRule>() where TRule : CompositeRule, new()
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