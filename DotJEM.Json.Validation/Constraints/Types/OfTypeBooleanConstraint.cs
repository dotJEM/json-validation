using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types
{
    [JsonConstraintDescription("a boolean (strict: {strict})")]
    public class OfTypeBooleanConstraint : JsonConstraint
    {
        private readonly bool strict;

        public OfTypeBooleanConstraint(bool strict)
        {
            this.strict = strict;
        }

        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            if (token == null) return false;
          
            //Note: If the token type matches we are happy regardless.
            if (token.Type == JTokenType.Boolean) return true;

            //Note: If the token type was not a match, and strict is enabled, return false.
            if (strict) return false;

            //Note: Only strings are allowed as an alternative. (for now, a integer could perhaps also make sense as 0 = false, 1 = true)
            if (token.Type != JTokenType.String) return false;

            bool boolean;
            return bool.TryParse((string)token, out boolean);
        }
    }
}