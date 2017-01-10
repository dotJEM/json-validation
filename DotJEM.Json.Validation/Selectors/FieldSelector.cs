using System.Collections.Generic;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Selectors
{
    public abstract class FieldSelector
    {
        private static readonly Regex arraySelector = new Regex(@".*\[\*|.+].*", RegexOptions.Compiled);
        public abstract string Alias { get; }
        
        public abstract IEnumerable<JToken> SelectTokens(JObject entity);

        public static implicit operator FieldSelector(string path)
        {
            return arraySelector.IsMatch(path)
                ? (PathBasedFieldSelector)new MultiFieldSelector(path)
                : new SingleFieldSelector(path);
        }
    }
}