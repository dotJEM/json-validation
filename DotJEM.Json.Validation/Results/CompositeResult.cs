using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public abstract class CompositeResult : AbstractResult
    {
        private readonly List<AbstractResult> results;

        public IEnumerable<AbstractResult> Results => results;

        protected CompositeResult(IEnumerable<AbstractResult> results)
        {
            this.results = results.ToList();
        }

        protected TResult OptimizeAs<TResult>() where TResult : CompositeResult, new()
        {
            return Results
                .Select(c => c.Optimize())
                .Aggregate(new TResult(), (c, next) =>
                {
                    TResult and = next as TResult;
                    if (and != null)
                    {
                        c.results.AddRange(and.Results);
                    }
                    else
                    {
                        c.results.Add(next);
                    }
                    return c;
                });
        }
    }
}