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
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public override IEnumerable<JTokenInfo> SelectTokens(JObject entity)
            => Selector.SelectTokens(entity);
    }
}