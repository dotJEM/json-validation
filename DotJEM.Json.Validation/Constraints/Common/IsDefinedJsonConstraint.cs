using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common
{
    [JsonConstraintDescription("defined.")]
    public class IsDefinedJsonConstraint : JsonConstraint
    {
        public override bool Matches(IJsonValidationContext context, JToken token)
        {
            return token != null && token.Type != JTokenType.Null && token.Type != JTokenType.Undefined;
        }
    }
}