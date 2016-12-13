using System.Linq;
using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using DotJEM.Json.Validation.Results;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints
{
    [JsonConstraintDescription("{Described}")]
    public sealed class OrJsonConstraint : CompositeJsonConstraint
    {
        public OrJsonConstraint()
        {
        }

        public OrJsonConstraint(params JsonConstraint[] constraints)
            : base(constraints)
        {
        }

        public override JsonConstraint Optimize()
        {
            return OptimizeAs<OrJsonConstraint>();
        }

        internal override AbstractResult DoMatch(IJsonValidationContext context, JToken token)
        {
            return Constraints.Aggregate((AbstractResult)null, (a, b) => a | b.DoMatch(context, token));
        }

        public override string ToString()
        {
            return "( " + string.Join(" OR ", Constraints.Select(c => c.Describe())) + " )";
        }

        // ReSharper disable UnusedMember.Local
        // Note: Used by description attribute
        private string Described => ToString();
        // ReSharper restore UnusedMember.Local
    }
}