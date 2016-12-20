using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Types
{
    [JsonConstraintDescription("of type object")]
    public class OfTypeObjectConstraint : JsonConstraint
    {
        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            return token.Type == JTokenType.Object;
        }
    }
}