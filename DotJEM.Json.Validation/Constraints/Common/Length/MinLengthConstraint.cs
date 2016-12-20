using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common.Length
{
    [JsonConstraintDescription("length more than or equal to '{minLength}'")]
    public class MinLengthConstraint : JsonConstraint
    {
        private readonly int minLength;

        public MinLengthConstraint(int minLength)
        {
            this.minLength = minLength;
        }

        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            JArray arr = token as JArray;
            if (arr != null)
                return arr.Count >= minLength;

            if (token.Type == JTokenType.String)
                return ((string)token).Length >= minLength;

            return false;
        }
    }
}