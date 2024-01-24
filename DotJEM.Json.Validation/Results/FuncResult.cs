namespace DotJEM.Json.Validation.Results;

public class FuncResult : Result
{
    public override bool IsValid { get; }
    public string Explain { get; }

    public FuncResult(bool value, string explain)
    {
        IsValid = value;
        Explain = explain;
    }
}