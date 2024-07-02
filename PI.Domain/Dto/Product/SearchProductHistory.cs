using PI.Domain.Common;
using PI.Domain.Enums;
using System.Text.Json.Serialization;

namespace PI.Domain.Dto.Product
{
    public class SearchProductHistory : SearchBaseRequest
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ShipmentType? ShipmentStatus { get; set; } = null;
        public DateTime? fromDate { get; set; } = null;
        public DateTime? toDate { get; set; } = null;
    }
}