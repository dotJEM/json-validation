using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Rules;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Results
{
    public abstract class AbstractResult
    {
        public abstract bool Value { get; }

        public virtual AbstractResult Optimize()
        {
            return this;
        }

        public static AbstractResult operator &(AbstractResult x, AbstractResult y)
        {
            //TODO: (jmd 2015-11-03) IF either is already a Or construct, we can reuse that and save the optimize. 

            if (x == null)
                return y;

            if (y == null)
                return x;

            return new AndResult(x, y);
        }

        public static AbstractResult operator |(AbstractResult x, AbstractResult y)
        {
            //TODO: (jmd 2015-11-03) IF either is already a Or construct, we can reuse that and save the optimize. 

            if (x == null)
                return y;

            if (y == null)
                return x;

            return new OrResult(x, y);
        }

        public static AbstractResult operator !(AbstractResult x)
        {
            return new NotResult(x);
        }
    }

    public abstract class CompositeResult : AbstractResult
    {
        private readonly List<AbstractResult> results;

        protected IEnumerable<AbstractResult> Results => results;

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

    public class AndResult : CompositeResult
    {
        public override bool Value => Results.All(r => r.Value);

        public AndResult() : base(new List<AbstractResult>())
        {
        }

        public AndResult(params AbstractResult[] results) : base(results)
        {
        }

        public AndResult(IEnumerable<AbstractResult> results) : base(results)
        {
        }

        public override AbstractResult Optimize() => base.OptimizeAs<AndResult>();
    }

    public class OrResult : CompositeResult
    {
        public override bool Value => Results.Any(r => r.Value);

        public OrResult() : base(new List<AbstractResult>())
        {
        }

        public OrResult(params AbstractResult[] results) : base(results)
        {
        }

        public OrResult(List<AbstractResult> results) : base(results)
        {
        }

        public override AbstractResult Optimize() => base.OptimizeAs<OrResult>();
    }

    public class NotResult : AbstractResult
    {
        public AbstractResult Result { get; }

        public override bool Value => !Result.Value;

        public NotResult(AbstractResult result)
        {
            Result = result;
        }

        public override AbstractResult Optimize()
        {
            NotResult not = Result as NotResult;
            return not != null ? not.Result : base.Optimize();
        }
    }

    public class Result : AbstractResult
    {
        public override bool Value { get; }

        public Result(bool value)
        {
            Value = value;
        }
    }

    public abstract class Result<T> : Result
    {
        protected Result(T context, bool value) : base(value)
        {
        }
    }

    public class ConstraintResult : Result<JsonConstraint>
    {
        public ConstraintResult(JsonConstraint context, JToken token, bool value) : base(context, value)
        {
        }
    }

    public class AnyResult : Result
    {
        public AnyResult() : base(true)
        {
        }
    }


    public class RuleResult : Result<JsonRule>
    {
        public RuleResult(JsonRule context, AbstractResult result) : base(context, result.Value)
        {
        }
    }

}
