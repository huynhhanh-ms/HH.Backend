using PI.Domain.Enums;
using System.Text.Json.Serialization;

namespace PI.Domain.Dto.ImportRequest
{
    public class ImportRequestResponse
    {
        public int ImportRequestId { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ImportRequestStatus ImportRequestStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public IEnumerable<string> ProductNames { get; set; } = new List<string>();

        public int CreatedBy { get; set; }
    }
}
