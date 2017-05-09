using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common.Length
{
    [JsonConstraintDescription("length less than or equal to '{maxLength}'")]
    public class MaxLengthConstraint : JsonConstraint
    {
        private readonly int maxLength;

        public MaxLengthConstraint(int maxLength)
        {
            this.maxLength = maxLength;
        }

        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            //TODO: Consider if null as ignored should be a specific base constraint.
            if (token == null || token.Type == JTokenType.Null || token.Type == JTokenType.Undefined)
                return true;

            JArray arr = token as JArray;
            if (arr != null)
                return arr.Count <= maxLength;

            if (token.Type == JTokenType.String)
                return ((string)token).Length <= maxLength;

            string value = (string)token;
            return value.Length <= maxLength;
        }
    }
}