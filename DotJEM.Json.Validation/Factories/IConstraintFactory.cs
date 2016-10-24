using System.Diagnostics;
using DotJEM.Json.Validation.Constraints;

namespace DotJEM.Json.Validation.Factories
{
    public interface IConstraintFactory
    {
        CapturedConstraint Capture(JsonConstraint constraint);
    }
    public interface IHaveConstraintFactory : IConstraintFactory
    {
    }

    public interface IBeConstraintFactory : IConstraintFactory
    {
    }

    public interface IGuardConstraintFactory : IBeConstraintFactory, IHaveConstraintFactory
    {
    }

    public class ConstraintFactory : IGuardConstraintFactory
    {
        private readonly string verb;
        private readonly ConstraintFactory pre;

        public ConstraintFactory(ConstraintFactory pre, string verb)
        {
            this.pre = pre;
            this.verb = verb;
        }

        public CapturedConstraint Capture(JsonConstraint constraint)
        {
            string stack = new StackTrace().ToString();

            string salutation = pre != null ? pre.verb + " " + this.verb : this.verb;

            return new CapturedConstraint(constraint);
        }
    }

    public sealed class CapturedConstraint
    {
        public JsonConstraint Constraint { get; }

        public CapturedConstraint(JsonConstraint constraint)
        {
            Constraint = constraint;
        }

        public static CapturedConstraint operator &(CapturedConstraint x, CapturedConstraint y)
        {
            return new CapturedConstraint(x.Constraint & y.Constraint);
        }

        public static CapturedConstraint operator |(CapturedConstraint x, CapturedConstraint y)
        {
            return new CapturedConstraint(x.Constraint | y.Constraint);
        }

        public static CapturedConstraint operator !(CapturedConstraint x)
        {
            return new CapturedConstraint(!x.Constraint);
        }
    }

    public interface IValidatorConstraintFactory
    {
        IBeConstraintFactory Be { get; }
        IHaveConstraintFactory Have { get; }
        CapturedConstraint Capture(JsonConstraint constraint);
    }
    

    public class ValidatorConstraintFactory : ConstraintFactory, IValidatorConstraintFactory
    {
        public IBeConstraintFactory Be { get; }
        public IHaveConstraintFactory Have { get; }

        public ValidatorConstraintFactory(ConstraintFactory pre, string verb) : base(pre, verb)
        {
            Be = new ConstraintFactory(this, "Be");
            Have = new ConstraintFactory(this, "Have");
        }
    }

    public interface ISelfReferencingRule
    {
    }

    public class SelfReferencingRule : ISelfReferencingRule
    {
    }

    //public static class CommonConstraintFactoryExtensions
    //{
    //    public static JsonConstraint Defined(this IGuardConstraintFactory self)
    //    {
    //        return new IsDefinedJsonConstraint();
    //    }
    //}

}