using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Rules.Results;

namespace DotJEM.Json.Validation
{
    public class JsonValidatorResult : Description
    {
        private readonly List<JsonRuleResult> results;

        public bool IsValid
        {
            get { return results.All(r => r.Value); }
        }

        public JsonValidatorResult(List<JsonRuleResult> results)
        {
            this.results = results;
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return results.Aggregate(writer, (w, r) => r.WriteTo(w));
        }
    }
}