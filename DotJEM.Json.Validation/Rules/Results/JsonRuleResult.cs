namespace DotJEM.Json.Validation.Rules.Results
{
    public abstract class JsonRuleResult
    {
        public abstract bool Value { get; }

        public virtual JsonRuleResult Optimize()
        {
            return this;
        }

        public static AndJsonRuleResult operator &(JsonRuleResult x, JsonRuleResult y)
        {
            return new AndJsonRuleResult(x, y);
        }

        public static OrJsonRuleResult operator |(JsonRuleResult x, JsonRuleResult y)
        {
            return new OrJsonRuleResult(x, y);
        }

        public static NotJsonRuleResult operator !(JsonRuleResult x)
        {
            return new NotJsonRuleResult(x);
        }

        public virtual JsonRuleResultDescription Describe()
        {
            return new JsonRuleResultDescription(this);
        }
    }

    public class JsonRuleResultDescription
    {
        public JsonRuleResultDescription(JsonRuleResult result)
        {
            
        }
    }
}
