using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public sealed class ValidatorResult : Result
    {
        private readonly List<Result> results;

        public IJsonValidator Context { get; }

        public override bool IsValid => Results.All(r => r.IsValid);

        public IEnumerable<Result> Results => results;

        public ValidatorResult(IJsonValidator context, params Result[] results) 
            : this(context, results.ToList())
        {
        }
        public ValidatorResult(IJsonValidator context, IEnumerable<Result> results)
            : this(context, results.ToList())
        {
        }

        public ValidatorResult(IJsonValidator context, List<Result> results)
        {
            Context = context;
            this.results = results;
        }
    }

    public sealed class FieldResult : Result
    {
        public override bool IsValid => !GuardResult.IsValid || ValidationResult.IsValid;

        public JsonFieldValidator Validator { get; }
        public Result GuardResult { get; }
        public Result ValidationResult { get; }

        public FieldResult(JsonFieldValidator validator, Result guardResult, Result validationResult)
        {
            Validator = validator;
            GuardResult = guardResult;
            ValidationResult = validationResult;
        }
    }
}