using System;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Results
{
    public sealed class BasicJsonConstraintResult : JsonConstraintResult
    {
        public JsonConstraintDescription Description { get; }
        public Type ConstraintType { get; private set; }

        public BasicJsonConstraintResult(bool value, JsonConstraintDescription description, Type constraintType)
            : base(value)
        {
            Description = description;
            ConstraintType = constraintType;
        }

        public override IDescriptionWriter WriteTo(IDescriptionWriter writer)
        {
            return Description.WriteTo(writer);
        }
    }
}