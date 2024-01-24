using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints;
using DotJEM.Json.Validation.Results;
using DotJEM.Json.Validation.Rules;
using DotJEM.Json.Validation.Visitors;

namespace DotJEM.Json.Validation.Descriptive;

public static class DescriptionExtensions
{
    public static IDescription Describe(this JsonConstraint self)
    {
        JsonConstraintDescriptionAttribute att = self.GetType()
            .GetCustomAttribute<JsonConstraintDescriptionAttribute>(false);

        return new JsonConstraintDescription(self, att?.Format);
    }

    //TODO: Return a IDescription and utilize that in the Descriptor/Visitor...
    public static string Describe(this Result self)
    {
        return self.Describe<DescribeFailurePath>();
    }

    //TODO: Return a IDescription and utilize that in the Descriptor/Visitor...
    public static string Describe<TDescriptor>(this Result self) where TDescriptor : AbstractDescriptor, new()
    {
        return new TDescriptor().Describe(self);
    }
}

public abstract class AbstractDescriptor : ResultVisitor
{
    //TODO: Would like a more stateless pattern where methods in the descriptor returns a value or takes a stringbuilder or html builder etc.
    //      This will do for now.
    private Indentation indent;
    private readonly StringBuilder builder;

    protected AbstractDescriptor()
    {
        indent = new Indentation(this);
        builder = new StringBuilder();
    }

    public string Describe(Result result)
    {
        builder.Clear();
        result = result.Optimize();
        result.Accept(this);
        return builder.ToString();
    }

    protected IDisposable Indent()
    {
        return indent = indent.Down();
    }

    protected AbstractDescriptor Write(string message)
    {
        builder.Append(message);
        return this;
    }

    protected AbstractDescriptor WriteLine(string message)
    {
        builder.AppendLine(indent + message);
        return this;
    }

    private class Indentation : IDisposable
    {
        private readonly int count;
        private readonly Indentation parent;
        private readonly AbstractDescriptor descriptor;

        public Indentation(AbstractDescriptor descriptor) : this(0, descriptor)
        {
        }

        private Indentation(int count, AbstractDescriptor descriptor, Indentation parent = null)
        {
            this.count = count;
            this.parent = parent;
            this.descriptor = descriptor;
        }

        public void Dispose()
        {
            descriptor.indent = parent;
        }

        public Indentation Down()
        {
            return new Indentation(count+1, descriptor, this);
        }

        public override string ToString()
        {
            return new string(' ', count*4);
        }
    }
}

public class DescribeFailurePath : AbstractDescriptor
{
    protected bool inguard = false;

    public override void Visit(Result result)
    {
        WriteLine(result.ToString());
    }

    public override void Visit(ConstraintExceptionResult visitee)
    {
        WriteLine($"{visitee.Constraint.ContextInfo} {visitee.Constraint.Describe()} failed with unkown errors. - actual value was: {visitee.Token ?? "NULL"}");
    }

    public override void Visit(ConstraintResult visitee)
    {
        WriteLine(inguard
            ? $"{visitee.Constraint.ContextInfo} {visitee.Constraint.Describe()}"
            : $"{visitee.Constraint.ContextInfo} {visitee.Constraint.Describe()} - actual value was: {visitee.Token ?? "NULL"}");
    }

    public override void Visit(LazyConstraintResult visitee)
    {
        WriteLine("compared to " + visitee.Other.Path);
        visitee.Result.Accept(this);
    }

    public override void Visit(FieldResult visitee)
    {
        if (!visitee.GuardResult.IsValid || visitee.ValidationResult.IsValid)
            return;

        if (!visitee.GuardResult.IsAny())
        {
            inguard = true;
            WriteLine("When");
            using (Indent())
            {
                visitee.GuardResult.Accept(this);
            }
            inguard = false;
            WriteLine("Then");
            using (Indent())
            {
                visitee.ValidationResult.Accept(this);
            }
        }
        else
        {
            WriteLine("Always");
            visitee.ValidationResult.Accept(this);
        }
        WriteLine("");
    }

    public override void Visit(AnyResult result)
    {
        WriteLine("ANY");
    }

    public override void Visit(RuleResult result)
    {
        if (result.Rule is BasicRule rule)
        {
            WriteLine($"{rule.Alias}");
        }
        base.Visit(result);
    }

    public override void Visit(FuncResult visitee)
    {
        Write(visitee.Explain);
    }

    public override void Visit(AndResult visitee)
    {
        List<Result> results = (inguard ? visitee.Results : visitee.Results.Where(r => !r.IsValid)).ToList();
        //results = visitee.Results.ToList();
        if (results.Count == 0)
            return;

        if (results.Count == 1)
        {
            results.First().Accept(this);
            return;
        }

        WriteLine("(");
        using (Indent())
        {
            bool first = true;
            foreach (Result child in visitee.Results)
            {
                if (!first)
                    WriteLine("AND");

                first = false;
                child.Accept(this);
            }
        }
        WriteLine(")");
    }


    public override void Visit(OrResult result)
    {
        WriteLine("(");
        using (Indent())
        {
            bool first = true;
            foreach (Result child in result.Results)
            {
                if (!first)
                    WriteLine("OR");

                first = false;
                child.Accept(this);
            }
        }
        WriteLine(")");
    }

    public override void Visit(NotResult result)
    {
        WriteLine("NOT");
        base.Visit(result);
    }
}


public static class ResultExt
{
    public static bool IsAny(this Result self)
    {
        switch (self)
        {
            case RuleResult rule:
                return rule.Result.IsAny();

            case ValidatorResult validator:
                if (validator.Results.Any(result => !result.IsAny()))
                    return false;

                break;

            case CompositeResult composite:
                if (composite.Results.Any(result => !result.IsAny()))
                    return false;

                break;
        }

        return self is AnyResult;
    }
}

public class JsonConstraintDescription : IDescription
{
    // { field or property, spacing : format }
    private static readonly Regex replacer = new Regex(@"\{\s*(?<field>\w+?(\.\w+)*)\s*(\,\s*(?<spacing>\-?\d+?))?\s*(\:\s*(?<format>.*?))?\s*\}", 
        RegexOptions.Compiled | RegexOptions.Multiline);

    private readonly Type type;
    private readonly JsonConstraint source;
    private readonly string format;

    public JsonConstraintDescription(JsonConstraint source, string format)
    {
        this.source = source;
        this.format = format;

        type = source.GetType();
    }

    public override string ToString()
    {
        return replacer.Replace(format, GetValue);
        //if (string.IsNullOrWhiteSpace(source.ContextInfo))
        //    return replacer.Replace(format, GetValue);
        //return source.ContextInfo + " " + replacer.Replace(format, GetValue);
    }
        
    public IDescriptionWriter WriteTo(IDescriptionWriter writer)
    {
        return writer.WriteLine(ToString());
    }

    private string GetValue(Match match)
    {
        string fieldOrProperty = match.Groups["field"].Value;
        string format = BuildFormat(match);

        FieldInfo field = type.GetField(fieldOrProperty, BindingFlags.NonPublic | BindingFlags.Instance);
        if (field != null)
        {
            return string.Format(format, field.GetValue(source));
        }

        PropertyInfo property = type.GetProperty(fieldOrProperty, BindingFlags.NonPublic | BindingFlags.Instance);
        if (property != null)
        {
            return string.Format(format, property.GetValue(source));
        }

        return "(UNKNOWN FIELD OR PROPERTY)";
    }


    private static string BuildFormat(Match match)
    {
        string spacing = match.Groups["spacing"].Value;
        string format = match.Groups["format"].Value;

        StringBuilder builder = new StringBuilder(64);
        builder.Append("{0");
        if (!string.IsNullOrEmpty(spacing))
        {
            builder.Append(",");
            builder.Append(spacing);
        }
        if (!string.IsNullOrEmpty(format))
        {
            builder.Append(":");
            builder.Append(format);
        }
        builder.Append("}");
        return builder.ToString();
    }
}