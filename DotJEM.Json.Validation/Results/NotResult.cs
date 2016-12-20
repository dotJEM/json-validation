namespace DotJEM.Json.Validation.Results
{
    public class NotResult : Result
    {
        public Result Result { get; }

        public override bool Value => !Result.Value;

        public NotResult(Result result)
        {
            Result = result;
        }

        public override Result Optimize()
        {
            NotResult not = Result as NotResult;
            return not != null ? not.Result : base.Optimize();
        }
    }
}