using PI.Domain.Dto.Account;
using PI.Domain.Dto.Category;
using PI.Domain.Dto.Manufacturer;
using PI.Domain.Dto.ProductAttribute;
using System.Text.Json.Serialization;

namespace PI.Domain.Dto.Product
{
    public class ProductResponse
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsAvailable { get; set; }

        public bool IsLowOnStock { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        //public long? TotalQuantity { get; set; } = null;

        public CategoryResponse Category { get; set; } = null!;

        public ManufacturerResponse Manufacturer { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int? MedicineId { get; set; } = null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? RegistrationNo { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? Indication { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public string? Medicinecol { get; set; }

        public string? PackingSize { get; set; }

        public float? NetWeight { get; set; }

        public List<ProductImageResponse> ProductImages { get; set; } = new List<ProductImageResponse>();
        
        // when array is empty not include in json
        public List<AttributeMappingResponse> ProductAttributeMappings { get; set; } = new List<AttributeMappingResponse>();

        // product unit
        public List<ProductUnitResponse> ProductUnits { get; set; } = new List<ProductUnitResponse>();
        
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("createdBy")] public AccountResponse CreatedByNavigation { get; set; } = null!;
        public DateTime UpdatedAt { get; set; }

        [JsonPropertyName("updatedBy")] public AccountResponse UpdatedByNavigation { get; set; } = null!;
    }
}