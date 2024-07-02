namespace PI.Domain.Dto.Lot
{
    public class CreateLotRequest
    {
        public string LotCode { get; set; } = null!;
        public DateTime ManufacturingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}