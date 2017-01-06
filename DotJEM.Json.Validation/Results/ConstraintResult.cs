using System;
using DotJEM.Json.Validation.Constraints;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Results
{
    public class ConstraintResult : Result
    {
        public override bool IsValid { get; }

        public JToken Token { get; }

        public JsonConstraint Constraint { get; }

        public ConstraintResult(JsonConstraint constraint, JToken token, bool value)
        {
            Constraint = constraint;
            Token = token;
            IsValid = value;
        }
    }

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
}