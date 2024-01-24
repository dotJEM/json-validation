using System;
using DotJEM.Json.Validation.Constraints;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Results;

public class ConstraintExceptionResult : Result
{
    public override bool IsValid => false;
    public JToken Token { get; }
    public Exception Exception { get; }
    public JsonConstraint Constraint { get; }

    public ConstraintExceptionResult(JsonConstraint constraint, JToken token, Exception exception)
    {
        Constraint = constraint;
        Token = token;
        Exception = exception;
    }
}