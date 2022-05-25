namespace Tickets.Filters
{
    public interface IJsonSchemaValidation
    {
        public bool JsonSchema(string json, string schema);
    }
}