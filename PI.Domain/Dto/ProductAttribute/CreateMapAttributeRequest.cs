namespace PI.Domain.Dto.ProductAttribute
{
    public class CreateMapAttributeRequest
    {
        public int ProductAttributeId { get; set; }
        public string Value { get; set; } = null!;
    }
}