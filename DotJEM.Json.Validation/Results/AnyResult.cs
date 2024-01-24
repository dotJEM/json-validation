namespace DotJEM.Json.Validation.Results;

public class AnyResult : Result
{
    public override bool IsValid => true;
}

public class SkippedResult : Result
{
    public override bool IsValid => true;
}