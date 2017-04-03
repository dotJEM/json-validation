using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common
{
    [JsonConstraintDescription("null or empty")]
    public class NullOrEmptyConstraint : JsonConstraint
    {
        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            switch (token?.Type)
            {
                case null:
                case JTokenType.Null:
                    return true;

                case JTokenType.Array:
                case JTokenType.Object:
                    return !token.HasValues;

                case JTokenType.String:
                    return string.IsNullOrEmpty((string)token);
            }
            return false;
        }
    }
}