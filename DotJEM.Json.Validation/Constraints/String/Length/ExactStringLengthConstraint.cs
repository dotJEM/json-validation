using DotJEM.Json.Validation.Context;
using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.String.Length;

[JsonConstraintDescription("length equal to '{length}'.")]
public class ExactStringLengthConstraint : TypedJsonConstraint<string>
{
    private readonly int length;

    public ExactStringLengthConstraint(int length)
    {
        this.length = length;
    }

    protected override bool Matches(string value, IJsonValidationContext context)
    {
        return value.Length == length;
    }
}