using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CityInfo.API.Enum
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ExpensiveCityType
    {
        Low,
        Medium,
        High
    }
}