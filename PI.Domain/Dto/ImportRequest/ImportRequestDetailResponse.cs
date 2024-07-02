namespace PI.Domain.Dto.ImportRequest
{
    public class ImportRequestDetailResponse
    {
        public int ImportRequestId { get; set; }
        public string? ImportRequestStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public IEnumerable<ImportRequestDetailItemResponse>? ImportRequestDetails { get; set; } = null;
    }

    public class ImportRequestDetailItemResponse
    {
        public int ImportRequestDetailId { get; set; }
        public int ProductUnitId { get; set; }
        public string SkuCode { get; set; } // product_unit
        public string Name { get; set; } // product_unit
        public string UnitName { get; set; } // product_unit => unit
        public int Quantity { get; set; }
    }
}
