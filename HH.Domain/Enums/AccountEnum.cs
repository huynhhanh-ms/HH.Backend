using System.Text.Json.Serialization;

namespace HH.Domain.Enums
{
    public enum AccountStatus
    {
        Active,
        Inactive,
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountRole
    {
        Guest,
        Staff,
        Manager,
        Admin
    }
}
