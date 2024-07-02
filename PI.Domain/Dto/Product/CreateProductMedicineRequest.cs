namespace PI.Domain.Dto.Product
{
    public class CreateProductMedicineRequest
    {
        public int MedicineId { get; set; }
        public string? Description { get; set; }
        public float? NetWeight { get; set; }
        public int CategoryId { get; set; }
        // public int MasterUnitId { get; set; }
        public List<CreateProductUnitRequest> Units { get; set; } = new List<CreateProductUnitRequest>();
        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}