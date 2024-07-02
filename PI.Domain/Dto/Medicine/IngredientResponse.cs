namespace PI.Domain.Dto.Medicine
{
    public class IngredientResponse
    {
        public int IngredientId { get; set; }
        public string? Description { get; set; }
        public string FullName { get; set; }
        public string? ShortName { get; set; }
    }
}