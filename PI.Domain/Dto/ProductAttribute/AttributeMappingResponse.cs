namespace PI.Domain.Dto.ProductAttribute
{
    public class AttributeMappingResponse
    {
        public int ProductAtMpId { get; set; }
        public string Value { get; set; } = null!;
        public ProductAttributeResponse ProductAttribute { get; set; } = null!;
    }
}