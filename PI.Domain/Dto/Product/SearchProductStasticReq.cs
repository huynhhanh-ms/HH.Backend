using PI.Domain.Enums;
using System.Text.Json.Serialization;

namespace PI.Domain.Dto.Product
{
    public class SearchProductStatisticReq
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductStatistic By { get; set; } = ProductStatistic.MONTH;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ShipmentType Type { get; set; } = ShipmentType.Import;
    }
}