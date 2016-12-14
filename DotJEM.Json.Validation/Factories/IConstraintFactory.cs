using System;
using System.Diagnostics;
using DotJEM.Json.Validation.Constraints;

namespace DotJEM.Json.Validation.Factories
{
    public interface IConstraintFactory
    {
        CapturedConstraint Capture(JsonConstraint constraint);
    }
    /// <summary>
    /// Guidance interface for supporting a fluent syntax.
    /// </summary>
    public interface IHaveConstraintFactory : IConstraintFactory { }

    /// <summary>
    /// Guidance interface for supporting a fluent syntax.
    /// </summary>
    public interface IBeConstraintFactory : IConstraintFactory { }

    /// <summary>
    /// Guidance interface for supporting a fluent syntax.
    /// </summary>
    public interface IGuardConstraintFactory : IBeConstraintFactory, IHaveConstraintFactory { }

    public class ConstraintFactory : IGuardConstraintFactory
    {
        private readonly string context;

        public ConstraintFactory(string context)
        {
            this.context = context;
        }

        public ConstraintFactory(ConstraintFactory pre, string context) : this(pre != null ? pre.context + " " + context : context)
        {
        }

        public CapturedConstraint Capture(JsonConstraint constraint)
        {
            return new CapturedConstraint(constraint, context);
        }
    }

    public sealed class CapturedConstraint
    {
        private readonly string context;
        public JsonConstraint Constraint { get; }

        public CapturedConstraint(JsonConstraint constraint, string context)
        {
            this.context = context;
            Constraint = constraint;
        }

        public static CapturedConstraint operator &(CapturedConstraint x, CapturedConstraint y)
        {
            return new CapturedConstraint(x.Constraint & y.Constraint, String.Empty);
        }

        public static CapturedConstraint operator |(CapturedConstraint x, CapturedConstraint y)
        {
            return new CapturedConstraint(x.Constraint | y.Constraint, String.Empty);
        }

        public static CapturedConstraint operator !(CapturedConstraint x)
        {
            return new CapturedConstraint(!x.Constraint, String.Empty);
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

        public ValidatorConstraintFactory(ConstraintFactory pre, string context) 
            : base(pre, context)
        {
            Be = new ConstraintFactory(this, "Be");
            Have = new ConstraintFactory(this, "Have");
        }
    }


    //public static class CommonConstraintFactoryExtensions
    //{
    //    public static JsonConstraint Defined(this IGuardConstraintFactory self)
    //    {
    //        return new IsDefinedJsonConstraint();
    //    }
    //}

}