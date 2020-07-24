using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PersonData
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EContactType
    {
        Privat,

        Geschäftlich
    }
}