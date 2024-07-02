using PI.Domain.Dto.Manufacturer;

namespace PI.Domain.Dto.Medicine
{
    public class MedicineResponse
    {
        public int MedicineId { get; set; }
        public string RegistrationNo { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? PackingSize { get; set; }
        public string? Indication { get; set; }
        public string? Medicinecol { get; set; }
        public List<IngredientResponse> Ingredients { get; set; } = new List<IngredientResponse>();
        public ManufacturerResponse? Manufacturer { get; set; }
    }
}