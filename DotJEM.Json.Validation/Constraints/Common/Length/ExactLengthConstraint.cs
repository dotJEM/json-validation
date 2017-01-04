using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common.Length
{
    [JsonConstraintDescription("length equal to '{length}'")]
    public class ExactLengthConstraint : JsonConstraint
    {
        private readonly int length;

        public ExactLengthConstraint(int length)
        {
            this.length = length;
        }

        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            if (token == null) return false;

            JArray arr = token as JArray;
            if (arr != null)
                return arr.Count == length;

            if (token.Type == JTokenType.String)
                return ((string) token).Length == length;

            return false;
        }
    }
}