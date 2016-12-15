using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types
{
    [JsonConstraintDescription("of type number (strict: {strict})")]
    public class OfTypeNumberJsonConstraint : JsonConstraint
    {
        private readonly bool strict;

        public OfTypeNumberJsonConstraint(bool strict)
        {
            this.strict = strict;
        }

        public override bool Matches(IJsonValidationContext context, JToken token)
        {
            //Note: If the token type matches we are happy regardless.
            if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer) return true;

            //Note: If the token type was not a match, and strict is enabled, return false.
            if (strict) return false;

            //Note: Only strings are allowed as an alternative.
            if (token.Type != JTokenType.String) return false;

            double number;
            return double.TryParse((string)token, out number);
        }
    }
}