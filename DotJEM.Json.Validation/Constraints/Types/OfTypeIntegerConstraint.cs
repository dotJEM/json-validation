using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types
{
    [JsonConstraintDescription("an integer (strict: {strict})")]
    public class OfTypeIntegerConstraint : JsonConstraint
    {
        private readonly bool strict;

        public OfTypeIntegerConstraint(bool strict)
        {
            this.strict = strict;
        }

        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            if (token == null) return false;
         
            //Note: If the token type matches we are happy regardless.
            if (token.Type == JTokenType.Integer) return true;

            //Note: If the token type was not a match, and strict is enabled, return false.
            if (strict) return false;

            //Note: Only strings are allowed as an alternative.
            if (token.Type != JTokenType.String) return false;

            int number;
            return int.TryParse((string) token, out number);
        }
    }
}