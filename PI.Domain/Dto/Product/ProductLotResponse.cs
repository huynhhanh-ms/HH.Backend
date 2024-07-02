using PI.Domain.Enums;

namespace PI.Domain.Dto.Product
{
    public class ProductLotResponse
    {
        public int LotId { get; set; }
        public string LotCode { get; set; } = null!;
        public long StockQuantity { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public LotStatus LotStatus { get; set; }
        public int ProductUnitId { get; set; }
    }
}