using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String.Length;

[JsonConstraintDescription("length from '{minLength}' to '{maxLength}'.")]
public class StringLengthConstraint : TypedJsonConstraint<string>
{
    private readonly int minLength;
    private readonly int maxLength;

    public StringLengthConstraint(int minLength, int maxLength)
    {
        this.minLength = minLength;
        this.maxLength = maxLength;
    }

    protected override bool Matches(string value, IJsonValidationContext context)
    {
        return value.Length >= minLength && value.Length <= maxLength;
    }
}