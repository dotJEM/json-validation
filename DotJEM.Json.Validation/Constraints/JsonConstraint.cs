using System.Linq;
using DotJEM.Json.Validation.Constraints.Results;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints
{
    public abstract class JsonConstraint : IDescribable
    {
        private readonly JsonConstraintDescriptionAttribute description;

        protected JsonConstraint()
        {
            description = GetType()
                .GetCustomAttributes(typeof (JsonConstraintDescriptionAttribute), false)
                .OfType<JsonConstraintDescriptionAttribute>()
                .SingleOrDefault();

            //if (description == null)
            //{
            //    throw new InvalidOperationException("JsonConstraints must have a JsonConstraintDescription attribute.");
            //}
        }

        public abstract bool Matches(IJsonValidationContext context, JToken token);

        internal virtual JsonConstraintResult DoMatch(IJsonValidationContext context, JToken token)
        {
            return new BasicJsonConstraintResult(Matches(context, token), Describe(context, token), GetType());
        }

        public virtual IDescription Describe()
        {
            return new JsonConstraintDescription(this, description.Format);
        }

        public virtual JsonConstraintDescription Describe(IJsonValidationContext context, JToken token)
        {
            return new JsonConstraintDescription(this, description.Format);
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

    public abstract class TypedJsonConstraint<TTokenType> : JsonConstraint
    {
        public override bool Matches(IJsonValidationContext context, JToken token)
        {
            return token == null
                ? Matches(context, default(TTokenType), true)
                : Matches(context, token.ToObject<TTokenType>());
        }

        protected virtual bool Matches(IJsonValidationContext context, TTokenType value)
        {
            return Matches(context, value, false);
        }

        protected virtual bool Matches(IJsonValidationContext context, TTokenType value, bool wasNull)
        {
            return true;
        }
    }
}