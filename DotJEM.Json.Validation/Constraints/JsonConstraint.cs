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
        //private readonly JsonConstraintDescriptionAttribute description;

        //protected JsonConstraint()
        //{
        //    //description = GetType()
        //    //    .GetCustomAttributes(typeof (JsonConstraintDescriptionAttribute), false)
        //    //    .OfType<JsonConstraintDescriptionAttribute>()
        //    //    .SingleOrDefault();

        //    //if (description == null)
        //    //{
        //    //    throw new InvalidOperationException("JsonConstraints must have a JsonConstraintDescription attribute.");
        //    //}
        //}

        public abstract bool Matches(IJsonValidationContext context, JToken token);

        internal virtual AbstractResult DoMatch(JToken token, IJsonValidationContext context)
        {
            return new ConstraintResult(this, token, Matches(context, token));
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