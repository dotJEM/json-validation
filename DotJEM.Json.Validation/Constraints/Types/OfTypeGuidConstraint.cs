using System;
using System.Globalization;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types;

[JsonConstraintDescription("a guid")]
public class OfTypeGuidConstraint : JsonConstraint
{
    public override bool Matches(JToken token, IJsonValidationContext context)
    {
        //TODO: Should null return true or falls, an array can be null after all.
        if (token == null)
            return false;

        if (token.Type == JTokenType.Guid)
            return true;

        if (token.Type != JTokenType.String)
            return false;

        Guid value;
        return Guid.TryParse((string) token, out value);
    }
}