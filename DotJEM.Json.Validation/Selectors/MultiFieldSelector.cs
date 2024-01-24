using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Selectors;

public class MultiFieldSelector : PathBasedFieldSelector
{
    public MultiFieldSelector(string path) 
        : base(path)
    {
    }

    public override IEnumerable<JTokenInfo> SelectTokens(JObject entity)
    {
        if (entity == null)
            return Enumerable.Empty<JTokenInfo>();

        return entity.SelectTokens(Path).Select(token => new JTokenInfo(token, this));
    }

    public override string ToString()
    {
        return $"Multiple:{Path}";
    }
}