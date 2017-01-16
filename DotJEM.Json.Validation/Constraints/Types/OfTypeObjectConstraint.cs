using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types
{
    [JsonConstraintDescription("an object")]
    public class OfTypeObjectConstraint : JsonConstraint
    {
        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            //TODO: Should null return true or falls, an object can be null after all.
            return token != null && token.Type == JTokenType.Object;
        }
    }
}