namespace DotJEM.Json.Validation.Results;

public class NotResult : Result
{
    public Result Result { get; }

    public override bool IsValid => !Result.IsValid;

    public NotResult(Result result)
    {
        Result = result;
    }

    public override Result Optimize()
    {
        return Result is NotResult not ? not.Result.Optimize() : new NotResult(Result.Optimize());
    }
}