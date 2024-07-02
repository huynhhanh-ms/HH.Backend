using System.Text.Json.Serialization;
using PI.Domain.Dto.Account;
using PI.Domain.Dto.Distributor;

namespace PI.Domain.Dto.Shipment
{
    public class ImportShipmentResponse
    {
        public int ShipmentId { get; set; }
        public long? TotalUnitProductQuantity { get; set; }
        public long? TotalPrice { get; set; }
        public string? ReceiptUrl { get; set; }
        public string? SignatureUrl { get; set; }
        public string? StorekeeperUrl { get; set; }
        public string? DistributorUrl { get; set; }
        public string? InvoiceUrl { get; set; }
        public DistributorResponse? Distributor { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("createdBy")]
        public AccountResponse CreatedByNavigation { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("updatedBy")]
        public AccountResponse UpdatedByNavigation { get; set; } = null!;
    }
}