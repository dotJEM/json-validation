using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types
{
    [JsonConstraintDescription("of type number")]
    public class OfTypeNumberJsonConstraint : JsonConstraint
    {
        private readonly bool strict;

        public OfTypeNumberJsonConstraint(bool strict)
        {
            this.strict = strict;
        }

        public override bool Matches(IJsonValidationContext context, JToken token)
        {
            if (strict) return token.Type == JTokenType.Float || token.Type == JTokenType.Integer;

            try
            {
                double number = (double) token;
                return true;
            }
            catch (Exception)
            {
                return false;                
            }
        }
    }
}