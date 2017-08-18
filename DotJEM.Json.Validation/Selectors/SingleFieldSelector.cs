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

        public override IEnumerable<JTokenInfo> SelectTokens(JObject entity)
        {
            if (entity == null)
                return new JTokenInfo[] {null};

            return new[] { new JTokenInfo(entity.SelectToken(Path), this),  };
        }

        public override string ToString()
        {
            return $"Single:{Path}";
        }
    }
}