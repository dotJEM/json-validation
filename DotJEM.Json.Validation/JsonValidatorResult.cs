using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Rules.Results;

namespace DotJEM.Json.Validation
{
    public class JsonValidatorResult
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

        public JsonValidatorResultDescription Describe()
        {
            return new JsonValidatorResultDescription(results.Where(r=>!r.Value).Select(r => r.Describe()));
        }
    }
}