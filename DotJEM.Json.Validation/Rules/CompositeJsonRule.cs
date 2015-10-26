using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public abstract class CompositeJsonRule : JsonRule
    {
        public List<JsonRule> Rules { get; }

        protected CompositeJsonRule(params JsonRule[] rules)
        {
            Rules = rules.ToList();
        }

        protected TRule OptimizeAs<TRule>() where TRule : CompositeJsonRule, new()
        {
            return Rules
                .Select(c => c.Optimize())
                .Aggregate(new TRule(), (c, next) =>
                {
                    TRule and = next as TRule;
                    if (and != null)
                    {
                        c.Rules.AddRange(and.Rules);
                    }
                    else
                    {
                        c.Rules.Add(next);
                    }
                    return c;
                });
        }
    }
}