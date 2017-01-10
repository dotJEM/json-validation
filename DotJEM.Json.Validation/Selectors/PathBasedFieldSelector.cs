using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotJEM.Json.Validation.Selectors
{
    public abstract class PathBasedFieldSelector : FieldSelector
    {
        public string Path { get; }
        public override string Alias => Path;

        protected PathBasedFieldSelector(string path)
        {
            Path = path;
        }

    }
}
