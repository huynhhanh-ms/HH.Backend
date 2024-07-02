namespace PI.Domain.Dto.Shipment
{
    public class ImportShipmentRequest
    {
        public List<ImportProductLotRequest> ProductLots { get; set; } = new();
        public int DistributorId { get; set; }
        public string? ReceiptUrl { get; set; }
        public string? SignatureUrl { get; set; }
        public string? StorekeeperUrl { get; set; }
        public string? DistributorUrl { get; set; }
        public string? InvoiceUrl { get; set; }
        public int? ImportRequestId { get; set; } = null;
    }
}