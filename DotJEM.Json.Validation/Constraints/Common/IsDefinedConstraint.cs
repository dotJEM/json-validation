using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Constraints.Common;

[JsonConstraintDescription("defined")]
public class IsDefinedConstraint : JsonConstraint
{
    public override bool Matches(JToken token, IJsonValidationContext context)
    {
        return token != null;
    }
}