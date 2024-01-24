using System;

namespace DotJEM.Json.Validation.Selectors;

public class AliasedFieldSelector : AggregateFieldSelector
{
    public override string Alias { get; }

    public AliasedFieldSelector(string alias, FieldSelector selector) 
        : base(selector)
    {
        if (alias == null) throw new ArgumentNullException(nameof(alias));
        if (selector == null) throw new ArgumentNullException(nameof(selector));

        Alias = alias;
    }
}