namespace DotJEM.Json.Validation.Results
{
    public class FuncResult : Result
    {
        public override bool Value { get; }
        public string Explain { get; }

        public FuncResult(bool value, string explain)
        {
            Value = value;
            Explain = explain;
        }
    }
}