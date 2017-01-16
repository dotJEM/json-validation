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
}