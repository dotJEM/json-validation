using DotJEM.Json.Validation.Rules;

namespace DotJEM.Json.Validation.Visitors
{
    public interface IRuleVisitor :
        IRuleVisitor<Rule>,
        IRuleVisitor<NotRule>,
        IRuleVisitor<CompositeRule>,
        IRuleVisitor<AndRule>,
        IRuleVisitor<OrRule>,
        IRuleVisitor<EmbededValidatorRule>,
        IRuleVisitor<AnyRule>,
        IRuleVisitor<FuncRule>,
        IRuleVisitor<BasicRule>
    { }

    public interface IRuleVisitor<in TVisitee> : IGenericVisitor<TVisitee> where TVisitee : Rule { }
}