﻿using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace DotJEM.Json.Validation.Results;

[DebuggerDisplay("Result: IsValid={IsValid}")]
public abstract class Result
{
    //TODO: Value = IsValid and add HasErrors.
    public abstract bool IsValid { get; }
    public bool HasErrors => !IsValid;

    //TODO: Consider removing this. We should base our self on optimized constrants, that should result in an optimized result graph.
    public virtual Result Optimize()
    {
        return this;
    }

    public static Result operator &(Result x, Result y)
    {
        //TODO: (jmd 2015-11-03) IF either is already a Or construct, we can reuse that and save the optimize. 

        if (x == null)
            return y;

        if (y == null)
            return x;

        return new AndResult(x, y);
    }

    public static Result operator |(Result x, Result y)
    {
        //TODO: (jmd 2015-11-03) IF either is already a Or construct, we can reuse that and save the optimize. 

        if (x == null)
            return y;

        if (y == null)
            return x;

        return new OrResult(x, y);
    }

    public static Result operator !(Result x)
    {
        return new NotResult(x);
    }
}