using System;
using System.IO;

namespace DotJEM.Json.Validation.Descriptive;

public interface IDescriptionWriter
{
    IDisposable Indent();
    IDescriptionWriter Write(string format, params object[] arg);
    IDescriptionWriter WriteLine(string format, params object[] arg);
    IDescriptionWriter WriteLine();
}

public class DescriptionWriter : IDescriptionWriter
{
    private int indentation;
    private readonly TextWriter inner;

    public DescriptionWriter()
        : this(new StringWriter())
    {
    }

    public DescriptionWriter(TextWriter inner)
    {
        this.inner = inner;
    }

    public IDisposable Indent()
    {
        return new IndentationScope(this);
    }

    public IDescriptionWriter Write(string format, params object[] arg)
    {
        format = new string(' ', indentation) + format;
        inner.Write(format, arg);
        return this;
    }

    public IDescriptionWriter WriteLine(string format, params object[] arg)
    {
        format = new string(' ', indentation) + format;
        inner.WriteLine(format, arg);
        return this;
    }

    public IDescriptionWriter WriteLine()
    {
        inner.WriteLine();
        return this;
    }

    public override string ToString()
    {
        return inner.ToString();
    }

    private sealed class IndentationScope : IDisposable
    {
        private readonly DescriptionWriter writer;

        public IndentationScope(DescriptionWriter writer)
        {
            this.writer = writer;
            writer.indentation++;
        }

        public void Dispose()
        {
            writer.indentation--;
        }
    }
}