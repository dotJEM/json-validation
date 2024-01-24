using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints;

public sealed class NotJsonConstraint : JsonConstraint
{
    public JsonConstraint Constraint { get; }

    public NotJsonConstraint(JsonConstraint constraint)
    {
        Constraint = constraint;
    }

    public override JsonConstraint Optimize()
    {
        NotJsonConstraint not = Constraint as NotJsonConstraint;
        return not != null ? not.Constraint : base.Optimize();
    }

    public override Result DoMatch(JToken token, IJsonValidationContext context) 
        => !Constraint.DoMatch(token, context);

    public override bool Matches(JToken token, IJsonValidationContext context) 
        => !Constraint.Matches(token, context);

    public override string ToString()
    {
        return $"not {Constraint}";
    }
}