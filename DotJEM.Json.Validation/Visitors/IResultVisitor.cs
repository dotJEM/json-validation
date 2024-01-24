using DotJEM.Json.Validation.Results;

namespace DotJEM.Json.Validation.Visitors;

public interface IResultVisitor<in TVisitee> : IGenericVisitor<TVisitee> where TVisitee : Result { }

public interface IResultVisitor :
    IResultVisitor<Result>,
    IResultVisitor<FuncResult>,
    IResultVisitor<FieldResult>,
    IResultVisitor<CompositeResult>,
    IResultVisitor<AndResult>,
    IResultVisitor<SkippedResult>,
    IResultVisitor<OrResult>,
    IResultVisitor<NotResult>,
    IResultVisitor<AnyResult>,
    IResultVisitor<ConstraintResult>,
    IResultVisitor<LazyConstraintResult>,
    IResultVisitor<ConstraintExceptionResult>,
    IResultVisitor<RuleResult>,
    IResultVisitor<ValidatorResult>,
    IResultVisitor<EmbededValidatorResult>
{ }