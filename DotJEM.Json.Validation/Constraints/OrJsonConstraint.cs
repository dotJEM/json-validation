using System.Linq;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints;

public sealed class OrJsonConstraint : CompositeJsonConstraint
{
    public OrJsonConstraint()
    {
    }

    public OrJsonConstraint(params JsonConstraint[] constraints)
        : base(constraints)
    {
    }

    public override JsonConstraint Optimize()
    {
        return OptimizeAs<OrJsonConstraint>();
    }

    public override Result DoMatch(JToken token, IJsonValidationContext context)
        => Constraints.Select(c => c.DoMatch(token, context)).Aggregate((a,b) => a |b);
            

    public override string ToString()
        => string.Join(" OR ", Constraints);
}