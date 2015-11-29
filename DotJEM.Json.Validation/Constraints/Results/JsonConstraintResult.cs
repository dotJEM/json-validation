using DotJEM.Json.Validation.Descriptive;

namespace DotJEM.Json.Validation.Constraints.Results
{
    public abstract class JsonConstraintResult 
    {
        public virtual bool Value { get; private set; }

        protected JsonConstraintResult(bool value)
        {
            Value = value;
        }

        public virtual JsonConstraintResult Optimize()
        {
            return this;
        }

        #region Operator Overloads
        public static JsonConstraintResult operator &(JsonConstraintResult x, JsonConstraintResult y)
        {
            if (x == null)
                return y;

            if (y == null)
                return x;

            return new AndJsonConstraintResult(x, y);
        }

        public static JsonConstraintResult operator |(JsonConstraintResult x, JsonConstraintResult y)
        {
            if (x == null)
                return y;

            if (y == null)
                return x;

            return new OrJsonConstraintResult(x, y);
        }

        public static JsonConstraintResult operator !(JsonConstraintResult x)
        {
            return new NotJsonConstraintResult(x);
        }
        #endregion
    }
}
