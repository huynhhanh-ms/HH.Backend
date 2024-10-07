using System.Text.Json.Serialization;

namespace HH.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TankType
    {
        Gasoline,
        Diesel,
    }
}
