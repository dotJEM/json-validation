using Newtonsoft.Json.Linq;

namespace DotJEM.Json.Validation.Context
{
    public interface IJsonValidationContext
    {
        JObject Updated { get; }
        JObject Deleted { get; }
    }

    public class JsonValidationContext : IJsonValidationContext
    {
        public JObject Updated { get; }
        public JObject Deleted { get; }

        public JsonValidationContext(JObject updated, JObject deleted)
        {
            Updated = updated;
            Deleted = deleted;
        }
    }
}