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
                if (array != null)
                    return array.AsEnumerable();

                if(token != null)
                    return new[] {token};

                return Enumerable.Empty<JToken>();
            });
        }
    }
}