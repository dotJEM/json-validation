using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Selectors
{
    public abstract class AggregateFieldSelector : FieldSelector
    {
        public FieldSelector Selector { get; }

        protected AggregateFieldSelector(FieldSelector selector)
        {
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            Selector = selector;
        }

        public override IEnumerable<JToken> SelectTokens(JObject entity)
            => Selector.SelectTokens(entity);
    }

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
}