using PI.Domain.Enums;
using System.Text.Json.Serialization;

namespace PI.Domain.Dto.Account
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountRole Role { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AccountStatus Status { get; set; }
        public bool IsFree { get; set; }
        public int TaskCount { get; set; }
    }
}