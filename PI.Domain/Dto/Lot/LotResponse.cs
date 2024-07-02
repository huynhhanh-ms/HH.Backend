using PI.Domain.Dto.Distributor;
using PI.Domain.Enums;

namespace PI.Domain.Dto.Lot
{
    public class LotResponse
    {
        public int LotId { get; set; }
        public string LotCode { get; set; } = null!;
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public LotStatus LotStatus { get; set; }
        public DistributorResponse? Distributor { get; set; }
    }
}