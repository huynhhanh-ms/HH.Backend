using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Dto.ProductStock.ProductStockEstimate
{
    public class EstimateProductStockResponse
    {
        public int ProductUnitId { get; set; }
        public string SkuCode { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public int ActualStockQuantity { get; set; }
        public int ExportRequestQuantity { get; set; }
        public int EstimateStockQuantity { get; set; }
    }

    public class EstimateProductStockWithImportRequestResponse : EstimateProductStockResponse
    {
        public int ImportRequestQuantity { get; set; }
    }
}
