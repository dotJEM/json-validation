using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types
{
    [JsonConstraintDescription("of type integer")]
    public class OfTypeIntegerJsonConstraint : JsonConstraint
    {
        private readonly bool strict;

        public OfTypeIntegerJsonConstraint(bool strict)
        {
            this.strict = strict;
        }

        public override bool Matches(IJsonValidationContext context, JToken token)
        {
            if (strict) return token.Type == JTokenType.Integer;

            try
            {
                int number = (int)token;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}