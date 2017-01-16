using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Selectors
{
    public class ForEachInFieldSelector : AggregateFieldSelector
    {
        public override string Alias => "each in " + Selector.Alias;

        public ForEachInFieldSelector(FieldSelector selector) : base(selector)
        {
        }
        
        public override IEnumerable<JToken> SelectTokens(JObject entity)
        {
            return base.SelectTokens(entity).SelectMany(token =>
            {
                JArray array = token as JArray;
                return array != null ? array.AsEnumerable() : new[] {token};
            });
        }
    }
}