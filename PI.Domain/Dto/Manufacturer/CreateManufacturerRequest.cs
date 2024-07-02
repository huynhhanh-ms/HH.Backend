namespace PI.Domain.Dto.Manufacturer
{
    public class CreateManufacturerRequest
    {
        public string Name { get; set; } = null!;
        public string Nation { get; set; } = null!;
        public string? Address { get; set; }
    }
}