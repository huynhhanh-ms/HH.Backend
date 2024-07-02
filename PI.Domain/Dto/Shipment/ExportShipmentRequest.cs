namespace PI.Domain.Dto.Shipment
{
    public class ExportShipmentRequest
    {
        public List<ExportProductLotRequest> ProductLots { get; set; } = new();
        public int ExportRequestId { get; set; }
        public string? ReceiptUrl { get; set; }
        public string? SignatureUrl { get; set; }
        public string? InvoiceUrl { get; set; }
    }
}