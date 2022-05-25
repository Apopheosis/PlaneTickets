using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Tickets.Filters
{
    public class JsonSchemaValidation:IJsonSchemaValidation
    {
        public bool JsonSchema(string json, string schema)
        {
            var model = JObject.Parse(schema);
            var jsonObject = JSchema.Parse(json);
            bool val = model.IsValid(jsonObject);
            return val;
        }
    }
}