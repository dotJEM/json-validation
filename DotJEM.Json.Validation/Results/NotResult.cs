namespace DotJEM.Json.Validation.Results
{
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
            NotResult not = Result as NotResult;
            return not != null ? not.Result.Optimize() : new NotResult(Result.Optimize());
        }
    }
}