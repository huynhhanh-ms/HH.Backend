namespace PI.Domain.Dto.Category
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public bool HasChildren { get; set; }
        public int? ParentId { get; set; }
    }
}