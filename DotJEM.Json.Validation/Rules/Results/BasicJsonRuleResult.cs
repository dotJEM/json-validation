using DotJEM.Json.Validation.Constraints.Results;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Rules.Results
{
    public sealed class BasicJsonRuleResult : JsonRuleResult
    {
        private readonly JsonConstraintResult result;

        public override bool Value => result.Value;
        public string Selector { get; }
        public string Path { get; }

        public BasicJsonRuleResult(string selector, string path, JsonConstraintResult result)
        {
            Path = path;
            Selector = selector;

            this.result = result.Optimize();
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return writer.WriteLine($"{Path ?? Selector} = {Value}");
        }
    }
}