using System;

namespace DotJEM.Json.Validation.Visitors;

public abstract class JsonFieldValidatorVisitor : IJsonFieldValidatorVisitor
{
    public void Visit(JsonFieldValidator visitee)
    {
        throw new NotImplementedException($"No approriate visitor methods was found for type: {visitee.GetType()}.");
    }
}