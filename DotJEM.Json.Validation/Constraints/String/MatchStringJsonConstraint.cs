using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String
{
    [JsonConstraintDescription("match the expression: '{expression}'.")]
    public class MatchStringJsonConstraint : TypedJsonConstraint<string>
    {
        private readonly Regex expression;

        public MatchStringJsonConstraint(Regex expression)
        {
            this.expression = expression;
        }

        protected override bool Matches(IJsonValidationContext context, string value) => expression.IsMatch(value);
    }
}