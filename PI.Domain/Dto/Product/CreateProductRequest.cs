using PI.Domain.Dto.ProductAttribute;

namespace PI.Domain.Dto.Product
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? PackingSize { get; set; }
        public float? NetWeight { get; set; }
        public int CategoryId { get; set; }
        public int ManufacturerId { get; set; }
        public int MasterUnitId { get; set; }
        public List<CreateProductUnitRequest> Units { get; set; } = new List<CreateProductUnitRequest>();
        
        public List<string> ImageUrls { get; set; } = new List<string>();
        
        //TODO: Add more properties
        public List<CreateMapAttributeRequest> ProductAttributes { get; set; } = new List<CreateMapAttributeRequest>();
        
    }
}