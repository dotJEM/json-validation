using System;
using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Context;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints
{
    public abstract class CompositeJsonConstraint : JsonConstraint
    {
        public List<JsonConstraint> Constraints { get; private set; }

        protected CompositeJsonConstraint(params JsonConstraint[] constraints)
        {
            Constraints = constraints.ToList();
        }

        public override bool Matches(IJsonValidationContext context, JToken token)
        {
            throw new InvalidOperationException();
        }

        protected TConstraint OptimizeAs<TConstraint>() where TConstraint : CompositeJsonConstraint, new()
        {
            return Constraints
                .Select(c => c.Optimize())
                .Aggregate(new TConstraint(), (c, next) =>
                {
                    TConstraint and = next as TConstraint;
                    if (and != null)
                    {
                        c.Constraints.AddRange(and.Constraints);
                    }
                    else
                    {
                        c.Constraints.Add(next);
                    }
                    return c;
                });
        }
    }
}