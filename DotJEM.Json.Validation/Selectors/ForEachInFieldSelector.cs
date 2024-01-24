using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Selectors;

public class ForEachInFieldSelector : AggregateFieldSelector
{
    public override string Alias => "each in " + Selector.Alias;

    public ForEachInFieldSelector(FieldSelector selector) 
        : base(selector)
    {
    }
        
    public override IEnumerable<JTokenInfo> SelectTokens(JObject entity)
    {
        return base.SelectTokens(entity).SelectMany(token =>
        {
            if(token == null)
                return Enumerable.Empty<JTokenInfo>();

            JArray array = token.Token as JArray;
            if (array != null)
                return array.AsEnumerable().Select(t => new JTokenInfo(t, this));

            return new[] { token };
        });
    }
}