using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Selectors
{
    public class MultiFieldSelector : PathBasedFieldSelector
    {
        public MultiFieldSelector(string path) 
            : base(path)
        {
        }

        public override IEnumerable<JToken> SelectTokens(JObject entity)
        {
            return entity.SelectTokens(Path);
        }

        public override string ToString()
        {
            return $"Multiple:{Path}";
        }
    }
}