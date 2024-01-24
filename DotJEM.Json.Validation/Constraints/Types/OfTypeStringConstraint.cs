using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types;

[JsonConstraintDescription("a string")]
public class OfTypeStringConstraint : JsonConstraint
{
    public override bool Matches(JToken token, IJsonValidationContext context)
    {
        //TODO: Should null return true or falls, a string can be null after all.
        return token != null && token.Type == JTokenType.String;
    }
}