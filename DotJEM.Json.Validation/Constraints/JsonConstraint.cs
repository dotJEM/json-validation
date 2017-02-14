using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Factories;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Selectors;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints
{
    public abstract class JsonConstraint
    {
        public string ContextInfo { get; internal set; }

        //TODO: Matches and DoMatch is a bit of a mess here... And internal makes no sense.
        //      Essentially either both should be public or one should be public and the other protected.
        public abstract bool Matches(JToken token, IJsonValidationContext context);

        public virtual Result DoMatch(JToken token, IJsonValidationContext context)
        {
            try
            {
                return new ConstraintResult(this, token, Matches(token, context));
            }
            catch (Exception ex)
            {
                return new ConstraintExceptionResult(this, token, ex);
            }
        }

        public virtual JsonConstraint Optimize()
        {
            return this;
        }

        public override string ToString()
        {
            return this.Describe().ToString();
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

    //TODO: we have some description issues here for some reason.
    [JsonConstraintDescription("{selector} '{value}'")]
    public sealed class LazyConstraint : JsonConstraint
    {
        private readonly FieldSelector selector;
        private readonly Func<JToken, CapturedConstraint> factory;

        public LazyConstraint(FieldSelector selector, Func<JToken, CapturedConstraint> factory)
        {
            this.selector = selector;
            this.factory = factory;
        }

        public override Result DoMatch(JToken token, IJsonValidationContext context)
        {
            DynamicContext con = (DynamicContext)context;

            JToken other = con.SelectToken(selector);
            JsonConstraint constraint = factory(other).Constraint.Optimize();
            return new LazyConstraintResult(this, other, constraint.DoMatch(token, context));
        }

        public override bool Matches(JToken token, IJsonValidationContext context)
        {
            return DoMatch(token, context).IsValid;
        }

    }
}