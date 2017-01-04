namespace DotJEM.Json.Validation.Visitors
{
    public interface IJsonValidatorVisitor : IJsonValidatorVisitor<JsonValidator>{}

    public interface IJsonValidatorVisitor<in TVisitee> : IGenericVisitor<TVisitee> where TVisitee : JsonValidator { }
}