using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types;

[JsonConstraintDescription("a date-time value")]
public class OfTypeDateTimeConstraint : JsonConstraint
{
    public override bool Matches(JToken token, IJsonValidationContext context)
    {
        //TODO: Should null return true or falls, an array can be null after all.
        if (token == null)
            return false;

        if (token.Type == JTokenType.Date)
            return true;

        if (token.Type != JTokenType.String)
            return false;

        DateTime value;
        return DateTime.TryParse((string) token, out value);
    }
}