using System.Diagnostics;
using System.Linq;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints
{
    public abstract class JsonConstraint 
    {
        public string ContextInfo { get; internal set; }

        public abstract bool Matches(JToken token, IJsonValidationContext context);

        internal virtual Result DoMatch(JToken token, IJsonValidationContext context)
        {
            return new ConstraintResult(this, token, Matches(token, context));
        }

        public virtual JsonConstraint Optimize()
        {
            return this;
        }

        #region Operator Overloads
        public static JsonConstraint operator &(JsonConstraint x, JsonConstraint y)
        {
            return new AndJsonConstraint(x, y);
        }

        public static JsonConstraint operator |(JsonConstraint x, JsonConstraint y)
        {
            return new OrJsonConstraint(x, y);
        }

        public static JsonConstraint operator !(JsonConstraint x)
        {
            return new NotJsonConstraint(x);
        }
        #endregion
    }
}