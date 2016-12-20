using DotJEM.Json.Validation.Context;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints
{
    public abstract class TypedJsonConstraint<TTokenType> : JsonConstraint
    {
        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            return token == null
                ? Matches(default(TTokenType), true, context)
                : Matches(token.ToObject<TTokenType>(), context);
        }

        protected virtual bool Matches(TTokenType value, IJsonValidationContext context)
        {
            return Matches(value, false, context);
        }

        protected virtual bool Matches(TTokenType value, bool wasNull, IJsonValidationContext context)
        {
            return true;
        }
    }
}