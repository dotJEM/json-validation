using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common;

[JsonConstraintDescription("equal to '{value}'")]
public class EqualToConstraint : JsonConstraint
{
    private readonly object value;

    public EqualToConstraint(object value)
    {
        this.value = value;
    }

    public override bool Matches(JToken token, IJsonValidationContext context)
    {
        //TODO: This is a bit heavy for simple values, it only makes sense for objects and arrays.

        if (value == null)
        {
            return token == null || token.Type == JTokenType.Null;
        }
        return JToken.DeepEquals(token, JToken.FromObject(value));
    }
}