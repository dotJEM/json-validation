namespace DotJEM.Json.Validation.Visitors
{
    public interface IGenericVisitor<in TVisitee>
    {
        void Visit(TVisitee visitee);
    }
}