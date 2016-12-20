using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types
{
    [JsonConstraintDescription("of type array")]
    public class OfTypeArrayConstraint : JsonConstraint
    {
        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            return token.Type == JTokenType.Array;
        }
    }
}