namespace PI.Domain.Dto.Distributor
{
    public class CreateDistributorRequest
    {
        public string Name { get; set; } = null!;
        public string? Address { get; set; }
    }
}