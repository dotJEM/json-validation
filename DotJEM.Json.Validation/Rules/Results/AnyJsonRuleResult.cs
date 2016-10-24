using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Rules.Results
{
    public sealed class AnyJsonRuleResult : JsonRuleResult
    {
        public override bool Value { get; } = true;

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return writer;
        }
    }

}