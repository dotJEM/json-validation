using System;
using System.Collections.Generic;
using System.Linq;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;

namespace DotJEM.Json.Validation
{
    public class JsonValidatorResult 
    {
        private readonly List<AbstractResult> results;

        public bool IsValid
        {
            get { return results.All(r => r.Value); }
        }

        public JsonValidatorResult(List<AbstractResult> results)
        {
            this.results = results;
        }
        
    }
}