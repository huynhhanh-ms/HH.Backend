using PI.Domain.Dto.Distributor;
using PI.Domain.Dto.ExportRequest;
using PI.Domain.Enums;
using System.Text.Json.Serialization;

namespace PI.Domain.Dto.Product
{
    public class ProductHistoryResponse
    {
        public int ProductUnitId { get; set; }
        public int ShipmentId { get; set; }
        public long EndingStocks { get; set; }
        public long Quantity { get; set; }
        public long Price { get; set; }
        public DateTime? ShipmentDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ShipmentType ShipmentStatus { get; set; }
        public string ReceiptUrl { get; set; }
        public DistributorResponse? Distributor { get; set; } = null;
        public ExportRequestResponse? ExportRequest { get; set; } = null;
    }
}