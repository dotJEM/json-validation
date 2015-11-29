using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Results
{
    public sealed class NotJsonConstraintResult : JsonConstraintResult
    {
        public JsonConstraintResult Result { get; }

        public NotJsonConstraintResult(JsonConstraintResult result)
            : base(!result.Value)
        {
            Result = result;
        }

        public override JsonConstraintResult Optimize()
        {
            NotJsonConstraintResult not = Result as NotJsonConstraintResult;
            return not != null ? not.Result : base.Optimize();
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return Result.WriteTo(writer.Write("not "));
        }
    }
}