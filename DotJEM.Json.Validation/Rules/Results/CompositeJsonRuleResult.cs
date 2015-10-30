using System;
using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Descriptive;

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

        protected IDescriptionWriter JoinWriteTo(IDescriptionWriter writer, Func<JsonRuleResult, bool> filter, string join)
        {
            //TODO: (jmd 2015-10-30) Delegate. 
            IEnumerable<JsonRuleResult> filtered = Results.Where(filter);
            using (writer.Indent())
            {
                filtered.Aggregate(false, (notFirst, result) =>
                {
                    if (notFirst)
                        writer.Write(join);

                    result.WriteTo(writer);
                    return true;
                });
                return writer;
            }
        }
    }
}