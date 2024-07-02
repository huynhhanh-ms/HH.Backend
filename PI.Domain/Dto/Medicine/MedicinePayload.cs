namespace PI.Domain.Dto.Medicine
{
    public class MedicinePayload
    {
        public Dictionary<string, object> SoDangKyThuoc { get; set; } = new Dictionary<string, object>();
        public bool KichHoat { get; set; } = true;
        public int skipCount { get; set; } = 15;
        public int maxResultCount { get; set; } = 35;
        public object sorting { get; set; } = null;
    }
}