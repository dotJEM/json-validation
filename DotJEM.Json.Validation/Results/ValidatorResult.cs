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
}