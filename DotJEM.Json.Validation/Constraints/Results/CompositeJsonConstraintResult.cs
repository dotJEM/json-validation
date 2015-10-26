using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Constraints.Results
{
    public abstract class CompositeJsonConstraintResult : JsonConstraintResult
    {
        protected List<JsonConstraintResult> Results { get; private set; }
        
        protected CompositeJsonConstraintResult(bool value, List<JsonConstraintResult> results)
            : base(value)
        {
            Results = results;
        }

        protected TResult OptimizeAs<TResult>() where TResult : CompositeJsonConstraintResult, new()
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