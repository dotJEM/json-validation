using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public sealed class ValidatorResult : Result
    {
        public IJsonValidator Context { get; }
        private readonly List<Result> results;
        public override bool Value => IsValid;

        public bool IsValid => Results.All(r => r.Value);
        public bool HasErrors => !IsValid;

        public IEnumerable<Result> Results => results;

        public ValidatorResult(IJsonValidator context, List<Result> results)
        {
            Context = context;
            this.results = results;
        }
    }

    public sealed class FieldResult : Result
    {
        public override bool Value => !GuardResult.Value || ValidationResult.Value;

        public JsonFieldValidator Field { get; }
        public Result GuardResult { get; }
        public Result ValidationResult { get; }

        public FieldResult(JsonFieldValidator field, Result guardResult, Result validationResult)
        {
            this.Field = field;
            GuardResult = guardResult;
            ValidationResult = validationResult;
        }



    }
}