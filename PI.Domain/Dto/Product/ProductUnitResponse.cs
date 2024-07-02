using System.Text.Json.Serialization;
using PI.Domain.Dto.Unit;

namespace PI.Domain.Dto.Product
{
    public class ProductUnitResponse
    {
        public int ProductUnitId { get; set; }

        public IEnumerable<string>? ProductImage { get; set; } = null; // Product -> ProductImage

        //public double StockQuantity { get; set; } //2024-05-04

        public string SkuCode { get; set; } = null!;

        [JsonPropertyName("fullName")]
        public string Name { get; set; } = null!;

        public bool IsAvailable { get; set; }

        public int ProductId { get; set; }

        public UnitResponse Unit { get; set; } = null!;

        // public ProductResponse Product { get; set; } = null!;
    }
}