using DotJEM.Json.Validation.Context;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints
{
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