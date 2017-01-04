namespace DotJEM.Json.Validation.Visitors
{
    public interface IJsonFieldValidatorVisitor : IJsonFieldValidatorVisitor<JsonFieldValidator> { }

    public interface IJsonFieldValidatorVisitor<in TVisitee> : IGenericVisitor<TVisitee> where TVisitee : JsonFieldValidator { }
}