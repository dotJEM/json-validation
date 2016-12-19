using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public sealed class ValidatorResult : AbstractResult
    {
        public IJsonValidator Context { get; }
        private readonly List<AbstractResult> results;
        public override bool Value => IsValid;

        public bool IsValid => Results.All(r => r.Value);
        public bool HasErrors => !IsValid;

        public IEnumerable<AbstractResult> Results => results;

        public ValidatorResult(IJsonValidator context, List<AbstractResult> results)
        {
            Context = context;
            this.results = results;
        }

    }
}