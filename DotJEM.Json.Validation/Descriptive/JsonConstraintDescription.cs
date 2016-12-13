using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints;

namespace DotJEM.Json.Validation.Descriptive
{
    public static class JsonConstraintDesciptionExtensions
    {
        public static IDescription Describe(this JsonConstraint self)
        {
            JsonConstraintDescriptionAttribute att = self.GetType()
                .GetCustomAttribute<JsonConstraintDescriptionAttribute>(false);

            return new JsonConstraintDescription(self, att?.Format);
        }
    }

    public class JsonConstraintDescription : IDescription
    {
        // { field or property, spacing : format }
        private static readonly Regex replacer = new Regex(@"\{\s*(?<field>\w+?(\.\w+)*)\s*(\,\s*(?<spacing>\-?\d+?))?\s*(\:\s*(?<format>.*?))?\s*\}", 
            RegexOptions.Compiled | RegexOptions.Multiline);

        private readonly Type type;
        private readonly JsonConstraint source;
        private readonly string format;

        public JsonConstraintDescription(JsonConstraint source, string format)
        {
            this.source = source;
            this.format = format;

            type = source.GetType();
        }

        public override string ToString()
        {
            return replacer.Replace(format, GetValue);
        }

        public IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return writer.WriteLine(ToString());
        }

        private string GetValue(Match match)
        {
            string fieldOrProperty = match.Groups["field"].Value;
            string format = BuildFormat(match);

            FieldInfo field = type.GetField(fieldOrProperty, BindingFlags.NonPublic | BindingFlags.Instance);
            if (field != null)
            {
                return string.Format(format, field.GetValue(source));
            }

            PropertyInfo property = type.GetProperty(fieldOrProperty, BindingFlags.NonPublic | BindingFlags.Instance);
            if (property != null)
            {
                return string.Format(format, property.GetValue(source));
            }

            return "(UNKNOWN FIELD OR PROPERTY)";
        }


        private static string BuildFormat(Match match)
        {
            string spacing = match.Groups["spacing"].Value;
            string format = match.Groups["format"].Value;

            StringBuilder builder = new StringBuilder(64);
            builder.Append("{0");
            if (!string.IsNullOrEmpty(spacing))
            {
                builder.Append(",");
                builder.Append(spacing);
            }
            if (!string.IsNullOrEmpty(format))
            {
                builder.Append(":");
                builder.Append(format);
            }
            builder.Append("}");
            return builder.ToString();
        }
    }
}