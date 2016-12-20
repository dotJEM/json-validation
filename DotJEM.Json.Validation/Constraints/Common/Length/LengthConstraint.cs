using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common.Length
{
    [JsonConstraintDescription("length from '{minLength}' to '{maxLength}'")]
    public class LengthConstraint : JsonConstraint
    {
        private readonly int minLength;
        private readonly int maxLength;

        public LengthConstraint(int minLength, int maxLength)
        {
            this.minLength = minLength;
            this.maxLength = maxLength;
        }
        
        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            JArray arr = token as JArray;
            if (arr != null)
                return arr.Count >= minLength && arr.Count <= maxLength;

            if (token.Type != JTokenType.String)
                return false;

            string value = (string) token;
            return value.Length >= minLength && value.Length <= maxLength;
        }
    }
}