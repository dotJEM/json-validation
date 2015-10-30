using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Rules.Results
{
    public sealed class NotJsonRuleResult : JsonRuleResult
    {
        public JsonRuleResult Result { get; }

        public override bool Value => !Result.Value;

        public NotJsonRuleResult(JsonRuleResult result)
        {
            Result = result;
        }

        public override JsonRuleResult Optimize()
        {
            NotJsonRuleResult not = Result as NotJsonRuleResult;
            return not != null ? not.Result : base.Optimize();
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return Result.WriteTo(writer.Write("not "));
        }
    }
}