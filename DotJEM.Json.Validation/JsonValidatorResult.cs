using System;
using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;

namespace DotJEM.Json.Validation
{
    public sealed class JsonValidatorResult 
    {
        public bool IsValid => Results.All(r => r.Value);
        public bool HasErrors => !IsValid;

        public IEnumerable<AbstractResult> Results { get; }

        public JsonValidatorResult(List<AbstractResult> results)
        {
            this.Results = results;
        }
        
    }
}