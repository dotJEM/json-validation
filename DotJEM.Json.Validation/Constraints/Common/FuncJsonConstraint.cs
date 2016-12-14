using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common
{
    [JsonConstraintDescription("{explain}.")]
    public class FuncJsonConstraint : JsonConstraint
    {
        private readonly Func<IJsonValidationContext, JToken, bool> func;
        
        // ReSharper disable NotAccessedField.Local
        // Note: Used by the description.
        private readonly string explain;
        // ReSharper restore NotAccessedField.Local

        public FuncJsonConstraint(Func<IJsonValidationContext, JToken, bool> func, string explain)
        {
            this.func = func;
            this.explain = explain;
        }

        public override bool Matches(IJsonValidationContext context, JToken token) => func(context, token);
    }
}