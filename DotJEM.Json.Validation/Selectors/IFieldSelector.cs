using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Selectors
{
    public abstract class FieldSelector 
    {
        private static readonly Regex arraySelector = new Regex(@".*\[\*|.+].*", RegexOptions.Compiled);
        public string Path { get; }

        protected FieldSelector(string path)
        {
            Path = path;
        }

        public static implicit operator FieldSelector(string path)
        {
            return arraySelector.IsMatch(path) 
                ? (FieldSelector) new MultiFieldSelector(path) 
                : new SingleFieldSelector(path);
        }

        public abstract IEnumerable<JToken> SelectTokens(JObject entity);
    }

    public class MultiFieldSelector : FieldSelector
    {
        public MultiFieldSelector(string path) : base(path)
        {
        }

        public override IEnumerable<JToken> SelectTokens(JObject entity)
        {
            return new[] { entity.SelectToken(Path) };
        }
        public override string ToString()
        {
            return $"Multiple:{Path}";
        }
    }

    public class SingleFieldSelector : FieldSelector
    {
        public SingleFieldSelector(string path) : base(path)
        {
        }

        public override IEnumerable<JToken> SelectTokens(JObject entity)
        {
            return entity.SelectTokens(Path);
        }

        public override string ToString()
        {
            return $"Single:{Path}";
        }
    }
}
