using System;
using System.Diagnostics;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Context;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Factories;

public interface IConstraintFactory
{
    /// <summary>
    /// Captures a constraint within a given context so that Is, Must etc. is preserved correctly.
    /// </summary>
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
public interface IGuardConstraintFactory : IBeConstraintFactory, IHaveConstraintFactory, IIsConstrainFactory, IHasConstraintFactory { }

public interface IIsConstrainFactory : IBeConstraintFactory { }

public interface IHasConstraintFactory : IHaveConstraintFactory { }

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
    public JsonConstraint Constraint { get; }

    public CapturedConstraint(JsonConstraint constraint, string context = "")
    {
        Constraint = constraint;
        Constraint.ContextInfo = context;
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

    public ValidatorConstraintFactory(ConstraintFactory pre, string context) 
        : base(pre, context)
    {
        Be = new ConstraintFactory(this, "be");
        Have = new ConstraintFactory(this, "have");
    }
}