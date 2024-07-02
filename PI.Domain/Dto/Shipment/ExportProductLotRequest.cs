namespace PI.Domain.Dto.Shipment
{
    public class ExportProductLotRequest
    {
        public string SkuCode { get; set; } = null!;
        public string LotCode { get; set; } = null!;
        public int ExportQuantity { get; set; }
    }
}