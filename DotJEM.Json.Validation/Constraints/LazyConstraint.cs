using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Selectors;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints;

//TODO: we have some description issues here for some reason.
[JsonConstraintDescription("{selector} 'LAZY'")]
public sealed class LazyConstraint : JsonConstraint
{
    private readonly FieldSelector selector;
    private readonly Func<CompareContext, CapturedConstraint> factory;

    public LazyConstraint(FieldSelector selector, Func<CompareContext, CapturedConstraint> factory)
    {
        this.selector = selector;
        this.factory = factory;
    }

    public override Result DoMatch(JToken token, IJsonValidationContext context)
    {
        DynamicContext con = (DynamicContext)context;
        CompareContext compare = con.SelectToken(selector);
        try
        {
            JsonConstraint constraint = factory(compare).Constraint.Optimize();
            //TODO: As of now we only support a single token due to the conversion that happens below.
            return new LazyConstraintResult(this, compare, constraint.DoMatch(token, context));
        }
        catch (Exception ex)
        {
            //TODO: As of now we only support a single token due to the conversion that happens below.
            return new LazyConstraintResult(this, compare, new ConstraintExceptionResult(this, token, ex));
        }
    }

    public override bool Matches(JToken token, IJsonValidationContext context)
    {
        return DoMatch(token, context).IsValid;
    }

}