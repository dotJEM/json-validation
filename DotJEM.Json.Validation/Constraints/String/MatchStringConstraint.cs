using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String
{
    [JsonConstraintDescription("match the expression: '{expression}'.")]
    public class MatchStringConstraint : TypedJsonConstraint<string>
    {
        private readonly Regex expression;

        public MatchStringConstraint(Regex expression)
        {
            this.expression = expression;
        }

        protected override bool Matches(string value, IJsonValidationContext context)
        {
            return expression.IsMatch(value);
        }
    }
}