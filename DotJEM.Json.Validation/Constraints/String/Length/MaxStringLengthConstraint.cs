using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String.Length;

[JsonConstraintDescription("length less than or equal to '{maxLength}'.")]
public class MaxStringLengthConstraint : TypedJsonConstraint<string>
{
    private readonly int maxLength;

    public MaxStringLengthConstraint(int maxLength)
    {
        this.maxLength = maxLength;
    }

    protected override bool Matches(string value, IJsonValidationContext context)
    {
        return value.Length <= maxLength;
    }
}