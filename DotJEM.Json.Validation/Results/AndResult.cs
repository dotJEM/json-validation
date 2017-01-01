using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class AndResult : CompositeResult
    {
        public override bool IsValid => Results.All(r => r.IsValid);

        public AndResult() : base(new List<Result>())
        {
            StackTraceHelper.PrintParent($"EMPTY AND");
        }

        public AndResult(params Result[] results) : base(results)
        {
            StackTraceHelper.PrintParent($"results = {results.Length}");
        }

        public AndResult(IEnumerable<Result> results) : base(results)
        {
            StackTraceHelper.PrintParent($"results = {results.Count()}");

        }

        public override Result Optimize() => base.OptimizeAs<AndResult>();
    }

    public static class StackTraceHelper
    {
        public static void PrintParent(string msg)
        {
            //StackTrace trace = new StackTrace();

            //Console.WriteLine(msg);
            //Console.WriteLine(trace);
        }
    }
}