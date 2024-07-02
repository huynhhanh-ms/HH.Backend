using System.ComponentModel.DataAnnotations.Schema;

namespace PI.Domain.Dto.ProductStock
{
    public class ProductStockResponse
    {
        [Column("product_unit_id")]
        public int ProductUnitId { get; set; }
        [Column("quantity")] 
        public long StockQuantity { get; set; }
        [Column("sku_code")]
        public string? SkuCode { get; set; } = null;
        public string? ProductName { get; set; } = null;
    }
}
