namespace PI.Domain.Dto.Distributor
{
    public class DistributorResponse
    {
        public int DistributorId { get; set; }
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
    }
}