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
            //TODO: NullFalseConstraint and NullTrueConstraint or something.
            if (token == null) return false;

            JArray arr = token as JArray;
            if (arr != null)
                return arr.Count >= minLength;

            if (token.Type == JTokenType.String)
                return ((string)token).Length >= minLength;

            if (token.Type == JTokenType.Integer || token.Type == JTokenType.Float)
                return ((string)token).Length >= minLength;

            string value = (string)token;
            return value.Length >= minLength;
        }
    }
}