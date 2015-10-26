using System;

namespace DotJEM.Json.Validation.Descriptive
{
    public class JsonConstraintDescriptionAttribute : Attribute
    {
        public string Format { get; private set; }

        public JsonConstraintDescriptionAttribute(string format)
        {
            Format = format;
        }
    }
}