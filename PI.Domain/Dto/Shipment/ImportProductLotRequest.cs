namespace PI.Domain.Dto.Shipment
{
    public class ImportProductLotRequest
    {
        public string SkuCode { get; set; } = null!;
        public string LotCode { get; set; } = null!;
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Quantity { get; set; }
        public long Cost { get; set; }
        // public long Price { get; set; }
    }
}