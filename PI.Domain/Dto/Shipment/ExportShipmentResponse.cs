using System.Text.Json.Serialization;
using PI.Domain.Dto.Account;
using PI.Domain.Dto.ExportRequest;

namespace PI.Domain.Dto.Shipment
{
    public class ExportShipmentResponse
    {
        public int ShipmentId { get; set; }
        public long? TotalUnitProductQuantity { get; set; }
        public ExportRequestResponse ExportRequest { get; set; } = null!;
        public string? ReceiptUrl { get; set; }
        public string? SignatureUrl { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("createdBy")]
        public AccountResponse CreatedByNavigation { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("updatedBy")]
        public AccountResponse UpdatedByNavigation { get; set; } = null!;
    }
}