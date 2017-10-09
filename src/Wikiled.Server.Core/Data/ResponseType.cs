using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Wikiled.Server.Core.Data
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ResponseType
    {
        Ok,
        Error
    }
}
