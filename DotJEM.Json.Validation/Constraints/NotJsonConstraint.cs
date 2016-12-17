using System;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints
{
    public sealed class NotJsonConstraint : JsonConstraint
    {
        public JsonConstraint Constraint { get; private set; }

        public NotJsonConstraint(JsonConstraint constraint)
        {
            Constraint = constraint;
        }

        public override JsonConstraint Optimize()
        {
            NotJsonConstraint not = Constraint as NotJsonConstraint;
            return not != null ? not.Constraint : base.Optimize();
        }

        internal override AbstractResult DoMatch(JToken token, IJsonValidationContext context)
        {
            return !Constraint.DoMatch(token, context);
        }

        public override bool Matches(IJsonValidationContext context, JToken token)
        {
            throw new InvalidOperationException();
        }
    }
}