using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Selectors
{
    public class SingleFieldSelector : PathBasedFieldSelector
    {
        public SingleFieldSelector(string path) 
            : base(path)
        {
        }

        public override IEnumerable<JToken> SelectTokens(JObject entity)
        {
            return new[] { entity.SelectToken(Path) };
        }

        public override string ToString()
        {
            return $"Single:{Path}";
        }
    }
}