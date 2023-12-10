using System.Text.Json;
using System.Text.Json.Serialization;

namespace NAMERP
{
    internal static class JsonHelper
    {
        private static readonly JsonSerializerOptions _serializerOptions = new() {
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            //IncludeFields = true,
        };

        public static T? ToClass<T>(this Dictionary<string, object> from)
        {
            string raw = JsonSerializer.Serialize(from);
            return JsonSerializer.Deserialize<T>(raw, _serializerOptions);
        }
    }
}
