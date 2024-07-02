namespace PI.Domain.Dto.Category
{
    public class CategoryLimitResponse
    {
        public int CategorySettingId { get; set; }
        public CategoryResponse Category { get; set; }
        public int MaxQuantity { get; set; }
        public int MinQuantity { get; set; }
    }
}