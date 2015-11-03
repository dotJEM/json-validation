using DotJEM.Json.Validation.Constraints.Results;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules.Results
{
    public sealed class BasicJsonRuleResult : JsonRuleResult
    {
        private readonly JsonConstraintResult result;

        public override bool Value => result.Value;
        public string Selector { get; }
        public string Path { get; }
        public JToken Token { get; }

        public BasicJsonRuleResult(string selector, JToken token, JsonConstraintResult result)
        {
            Token = token;
            Path = token?.Path;
            Selector = selector;

            this.result = result.Optimize();
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            writer.Write($"{Path ?? Selector}: ");
            result.WriteTo(writer);
            writer.WriteLine($" but was: {Token}");
            return writer.WriteLine();
        }
    }
}