using DotJEM.Json.Validation.Constraints;

namespace DotJEM.Json.Validation.Visitors;

public interface IConstraintVisitor :
    IConstraintVisitor<JsonConstraint>,
    IConstraintVisitor<CompositeJsonConstraint>,
    IConstraintVisitor<NotJsonConstraint>,
    IConstraintVisitor<AndJsonConstraint>,
    IConstraintVisitor<OrJsonConstraint>
{ }

public interface IConstraintVisitor<in TVisitee> : IGenericVisitor<TVisitee> where TVisitee : JsonConstraint { }