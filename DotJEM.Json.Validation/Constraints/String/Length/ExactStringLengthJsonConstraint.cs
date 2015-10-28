using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String.Length
{
    //TODO: We need to figure out what the correct auxiliary verb is so we can correctly format this as:
    // When length is x then length must be y. So "must be" should be interchangeable with "is" here.
    //https://en.wikipedia.org/wiki/Modal_verb
    [JsonConstraintDescription("length must be '{length}'.")]
    public class ExactStringLengthJsonConstraint : TypedJsonConstraint<string>
    {
        private readonly int length;

        public ExactStringLengthJsonConstraint(int length)
        {
            this.length = length;
        }

        protected override bool Matches(IJsonValidationContext context, string value)
        {
            return value.Length == length;
        }
    }
}