using System;

namespace DotJEM.Json.Validation.Visitors;

public abstract class JsonValidatorVistor : IJsonValidatorVisitor
{
    public void Visit(JsonValidator visitee)
    {
        throw new NotImplementedException($"No approriate visitor methods was found for type: {visitee.GetType()}.");
    }
}