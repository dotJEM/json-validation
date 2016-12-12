using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Rules
{
    public abstract class JsonRule
    {
        public string RuleContext { get; internal set; }

        public abstract AbstractResult Test(IJsonValidationContext contenxt, JObject entity);
        
        public static AndJsonRule operator &(JsonRule x, JsonRule y)
        {
            return new AndJsonRule(x, y);
        }

        public static OrJsonRule operator |(JsonRule x, JsonRule y)
        {
            return new OrJsonRule(x, y);
        }

        public static NotJsonRule operator !(JsonRule x)
        {
            return new NotJsonRule(x);
        }

        public virtual JsonRule Optimize()
        {
            return this;
        }

        //public abstract Description Describe();

        public virtual TVisitor Accept<TVisitor>(TVisitor visitor) where TVisitor:IJsonRuleVisitor
        {
            return visitor.Visit((dynamic) this);
        }
    }

    public interface IJsonRuleVisitor
    {
        IJsonRuleVisitor Visit(JsonRule rule);
    }

    public interface IJsonRuleVisitor<in TRule> where TRule : JsonRule
    {
        IJsonRuleVisitor Visit(TRule rule);
    }

    public abstract class JsonRuleVisitor : IJsonRuleVisitor
    {
        public virtual IJsonRuleVisitor Visit(JsonRule rule) => this;

    }
}