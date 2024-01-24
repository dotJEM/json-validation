using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Visitors;

public static class VisitorExtensions
{
    public static TVisitor Accept<TVisitor>(this Result self, TVisitor visitor) where TVisitor : IResultVisitor
    {
        //Note: Dynamic dispatch forcing runtime resolution of the correct overload.
        visitor.Visit((dynamic)self);
        return visitor;
    }

    public static TVisitor Accept<TVisitor>(this Rule self, TVisitor visitor) where TVisitor : IRuleVisitor
    {
        //Note: Dynamic dispatch forcing runtime resolution of the correct overload.
        visitor.Visit((dynamic)self);
        return visitor;
    }

    public static TVisitor Accept<TVisitor>(this JsonConstraint self, TVisitor visitor) where TVisitor : IConstraintVisitor
    {
        //Note: Dynamic dispatch forcing runtime resolution of the correct overload.
        visitor.Visit((dynamic)self);
        return visitor;
    }

    public static TVisitor Accept<TVisitor>(this JsonFieldValidator self, TVisitor visitor) where TVisitor : IJsonFieldValidatorVisitor
    {
        //Note: Dynamic dispatch forcing runtime resolution of the correct overload.
        visitor.Visit((dynamic)self);
        return visitor;
    }

    public static TVisitor Accept<TVisitor>(this JsonValidator self, TVisitor visitor) where TVisitor : IJsonValidatorVisitor
    {
        //Note: Dynamic dispatch forcing runtime resolution of the correct overload.
        visitor.Visit((dynamic)self);
        return visitor;
    }
}