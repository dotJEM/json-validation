namespace DotJEM.Json.Validation.Results
{
    public class Result : AbstractResult
    {
        public override bool Value { get; }

        public Result(bool value)
        {
            Value = value;
        }
    }
}