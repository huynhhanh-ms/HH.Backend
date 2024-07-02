namespace PI.Domain.Dto.Category
{
    public class CategoryLimitRequest
    {
        public int CategoryId { get; set; }
        public int MaxQuantity { get; set; }
        public int MinQuantity { get; set; }
    }
}