﻿using System.Collections.Generic;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class AndResult : CompositeResult
    {
        public override bool Value => Results.All(r => r.Value);

        public AndResult() : base(new List<Result>())
        {
        }

        public AndResult(params Result[] results) : base(results)
        {
        }

        public AndResult(IEnumerable<Result> results) : base(results)
        {
        }

        public override Result Optimize() => base.OptimizeAs<AndResult>();
    }
}