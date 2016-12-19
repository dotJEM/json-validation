namespace DotJEM.Json.Validation.Results
{
    public class NotResult : AbstractResult
    {
        public AbstractResult Result { get; }

        public override bool Value => !Result.Value;

        public NotResult(AbstractResult result)
        {
            Result = result;
        }

        public override AbstractResult Optimize()
        {
            NotResult not = Result as NotResult;
            return not != null ? not.Result : base.Optimize();
        }
    }
}