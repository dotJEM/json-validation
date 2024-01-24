using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common;

[JsonConstraintDescription("value")]
public class HasValueConstraint : JsonConstraint
{
    public override bool Matches(JToken token, IJsonValidationContext context)
    {
        switch (token?.Type)
        {
            case null:
            case JTokenType.Null:
                return false;

            case JTokenType.Array:
            case JTokenType.Object:
                return token.HasValues;

            case JTokenType.String:
                return !string.IsNullOrEmpty((string) token);
        }
        return true;
    }
}