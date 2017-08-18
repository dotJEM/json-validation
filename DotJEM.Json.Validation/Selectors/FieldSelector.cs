using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotJEM.Json.Validation.Constraints.Types;
using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Selectors
{
    public abstract class FieldSelector
    {
        private static readonly Regex arraySelector = new Regex(@".*\[\*|.+].*", RegexOptions.Compiled);
        public abstract string Alias { get; }
        
        public abstract IEnumerable<JTokenInfo> SelectTokens(JObject entity);

        public static implicit operator FieldSelector(string path)
        {
            return arraySelector.IsMatch(path)
                ? (PathBasedFieldSelector)new MultiFieldSelector(path)
                : new SingleFieldSelector(path);
        }

        public static CompositeFieldSelector operator &(FieldSelector left, FieldSelector right)
        {
            CompositeFieldSelector leftComposite = left as CompositeFieldSelector;
            if (leftComposite != null)
                return leftComposite.Add(right);

            CompositeFieldSelector rightComposite = right as CompositeFieldSelector;
            if (rightComposite != null)
                return rightComposite.Add(left);

            return new CompositeFieldSelector(left, right);
        }
    }

    public class CompositeFieldSelector : FieldSelector
    {
        public override string Alias => Selectors.Select(selector => selector.Alias).Aggregate((acc, next) => $"{acc} & {next}");

        public List<FieldSelector> Selectors { get; }

        public CompositeFieldSelector(params FieldSelector[] selectors)
        {
            Selectors = new List<FieldSelector>(selectors.Length);
            foreach (FieldSelector selector in selectors)
                Add(selector);
        }

        public override IEnumerable<JTokenInfo> SelectTokens(JObject entity) => Selectors.SelectMany(selector => selector.SelectTokens(entity));

        public CompositeFieldSelector Add(FieldSelector selector)
        {
            CompositeFieldSelector composite = selector as CompositeFieldSelector;
            if (composite != null)
            {
                Selectors.AddRange(composite.Selectors);
            }
            return this;
        }
    }

    public class JTokenInfo
    {
        public JToken Token { get; }
        public FieldSelector Selector { get; }

        public JTokenInfo(JToken token, FieldSelector selector)
        {
            Token = token;
            Selector = selector;
        }

        public static implicit operator JToken(JTokenInfo info)
        {
            return info?.Token;
        }

        public static implicit operator JTokenInfo(JToken token)
        {
            return new JTokenInfo(token, null);
        }
    }
}