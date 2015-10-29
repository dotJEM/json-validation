using DotJEM.Json.Validation.Descriptive;

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

    public class CompositeRuleResultDescription : Description
    {
        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return writer;
        }
    }

    public class JsonRuleResultDescription : Description
    {
        private readonly JsonRuleResult result;

        public JsonRuleResultDescription(JsonRuleResult result)
        {
            this.result = result;
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            BasicJsonRuleResult basic = result as BasicJsonRuleResult;
            if (basic != null)
            {
                return writer.WriteLine($"{basic.Path ?? basic.Selector} was invalid");
            }

            NotJsonRuleResult not = result as NotJsonRuleResult;
            if (not != null)
            {
                

                return writer.WriteLine("Not failed " + result.Value + " failed.");
            }

            AndJsonRuleResult and = result as AndJsonRuleResult;
            if (and != null)
            {
                return writer.WriteLine("And failed " + result.Value + " failed.");
            }

            OrJsonRuleResult or = result as OrJsonRuleResult;
            if (or != null)
            {
                return writer.WriteLine("Or failed " + result.Value + " failed.");
            }

            return writer.WriteLine("N/A");
        }
    }
}
