using System;
using System.Collections.Generic;
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

        public CompareContext SelectToken(FieldSelector selector)
        {
            //TODO: If we in some way can do "Any(values, v => Must.Be.LessThan(v))" we can allow for array selectors here to work;
            return new CompareContext(this, selector.SelectTokens(root));
        }
    }

    public class CompareContext
    {
        private readonly DynamicContext context;
        private readonly IEnumerable<JTokenInfo> tokens;

        public CompareContext(DynamicContext context, IEnumerable<JTokenInfo> tokens)
        {
            this.context = context;
            this.tokens = tokens;
        }

        public static implicit operator JToken(CompareContext context)
        {
            return context.tokens.SingleOrDefault();
        }

        //NOTE: Compatibility!
        public static explicit operator bool(CompareContext value) => (bool)(JToken)value;
        public static explicit operator DateTimeOffset(CompareContext value) => (DateTimeOffset)(JToken)value;
        public static explicit operator bool? (CompareContext value) => (bool?)(JToken)value;
        public static explicit operator long(CompareContext value) => (long)(JToken)value;
        public static explicit operator DateTime? (CompareContext value) => (DateTime?)(JToken)value;
        public static explicit operator DateTimeOffset? (CompareContext value) => (DateTimeOffset?)(JToken)value;
        public static explicit operator Decimal? (CompareContext value) => (Decimal?)(JToken)value;
        public static explicit operator double? (CompareContext value) => (double?)(JToken)value;
        public static explicit operator char? (CompareContext value) => (char?)(JToken)value;
        public static explicit operator int(CompareContext value) => (int)(JToken)value;
        public static explicit operator short(CompareContext value) => (short)(JToken)value;
        public static explicit operator ushort(CompareContext value) => (ushort)(JToken)value;
        public static explicit operator char(CompareContext value) => (char)(JToken)value;
        public static explicit operator byte(CompareContext value) => (byte)(JToken)value;
        public static explicit operator sbyte(CompareContext value) => (sbyte)(JToken)value;
        public static explicit operator int? (CompareContext value) => (int?)(JToken)value;
        public static explicit operator short? (CompareContext value) => (short?)(JToken)value;
        public static explicit operator ushort? (CompareContext value) => (ushort?)(JToken)value;
        public static explicit operator byte? (CompareContext value) => (byte?)(JToken)value;
        public static explicit operator sbyte? (CompareContext value) => (sbyte?)(JToken)value;
        public static explicit operator DateTime(CompareContext value) => (DateTime)(JToken)value;
        public static explicit operator long? (CompareContext value) => (long?)(JToken)value;
        public static explicit operator float? (CompareContext value) => (float?)(JToken)value;
        public static explicit operator Decimal(CompareContext value) => (Decimal)(JToken)value;
        public static explicit operator uint? (CompareContext value) => (uint?)(JToken)value;
        public static explicit operator ulong? (CompareContext value) => (ulong?)(JToken)value;
        public static explicit operator double(CompareContext value) => (double)(JToken)value;
        public static explicit operator float(CompareContext value) => (float)(JToken)value;
        public static explicit operator string(CompareContext value) => (string)(JToken)value;
        public static explicit operator uint(CompareContext value) => (uint)(JToken)value;
        public static explicit operator ulong(CompareContext value) => (ulong)(JToken)value;
        public static explicit operator byte[] (CompareContext value) => (byte[])(JToken)value;
        public static explicit operator Guid(CompareContext value) => (Guid)(JToken)value;
        public static explicit operator Guid? (CompareContext value) => (Guid?)(JToken)value;
        public static explicit operator TimeSpan(CompareContext value) => (TimeSpan)(JToken)value;
        public static explicit operator TimeSpan? (CompareContext value) => (TimeSpan?)(JToken)value;
        public static explicit operator Uri(CompareContext value) => (Uri)(JToken)value;

    }
}