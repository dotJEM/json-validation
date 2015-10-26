using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Rules.Results
{
    public abstract class CompositeJsonRuleResult : JsonRuleResult
    {
        protected List<JsonRuleResult> Results { get; }
        
        protected CompositeJsonRuleResult(List<JsonRuleResult> results)
        {
            Results = results;
        }

        protected TResult OptimizeAs<TResult>() where TResult : CompositeJsonRuleResult, new()
        {
            return Results
                .Select(c => c.Optimize())
                .Aggregate(new TResult(), (c, next) =>
                {
                    TResult and = next as TResult;
                    if (and != null)
                    {
                        c.Results.AddRange(and.Results);
                    }
                    else
                    {
                        c.Results.Add(next);
                    }
                    return c;
                });
        }
    }
}