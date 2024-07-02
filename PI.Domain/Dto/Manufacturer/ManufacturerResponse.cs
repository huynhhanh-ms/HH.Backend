namespace PI.Domain.Dto.Manufacturer
{
    public class ManufacturerResponse
    {
        public int ManufacturerId { get; set; }
        public string Name { get; set; } = null!;
        public string Nation { get; set; } = null!;
        public string? Address { get; set; }
    }
}