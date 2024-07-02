namespace PI.Domain.Dto.Product.Import
{
    public class ReadImportProductExcelResponse
    {
        public string SKU { get; set; } 
        public string ProductName { get; set; }
        public string ProductUnit { get; set; }
        public string ProductLot { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductCost { get; set; }
    }
}
