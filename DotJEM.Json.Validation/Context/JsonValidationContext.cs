using System;
using System.Dynamic;
using System.Linq;
using DotJEM.Json.Validation.Selectors;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Context
{
    public interface IJsonValidationContext
    {
    }

    public class JsonValidationContext : IJsonValidationContext
    {
        public JsonValidationContext()
        {
        }
    }

    public class DynamicContext : DynamicObject, IJsonValidationContext
    {
        private readonly JObject root;
        private readonly Type contextType;
        public IJsonValidationContext InnerContext { get; }

        public DynamicContext(IJsonValidationContext context, JObject root)
        {
            this.InnerContext = context;
            this.root = root;
            this.contextType = context?.GetType();
        }


        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            
            if (InnerContext == null)
            {
                result = null;
                return true;
            }

            if (binder.Type.IsAssignableFrom(contextType))
            {
                result = InnerContext;
                return true;
            }

            if (typeof(DynamicContext).IsAssignableFrom(binder.Type))
            {
                result = this;
                return true;
            }

            if (!binder.Explicit)
            {
                result = null;
                return true;
            }

            return base.TryConvert(binder, out result);
        }

        public JToken SelectToken(FieldSelector selector)
        {
            //TODO: If we in some way can do "Any(values, v => Must.Be.LessThan(v))" we can allow for array selectors here to work;
            return selector.SelectTokens(root).SingleOrDefault();
        }
    }
}