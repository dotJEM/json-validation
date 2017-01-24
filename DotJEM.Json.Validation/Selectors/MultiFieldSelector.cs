using System.Collections.Generic;
using System.Linq;
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
            if (entity == null)
                return Enumerable.Empty<JToken>();

            return entity.SelectTokens(Path);
        }

        public override string ToString()
        {
            return $"Multiple:{Path}";
        }
    }

}