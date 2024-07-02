using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Dto.ExportRequest
{
    public class ExportRequestDetailResponse
    {
        public int ExportRequestId { get; set; }
        public string? ExportStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public IEnumerable<ExportRequestDetailItemResponse> ExportRequestDetails { get; set; }
    }

    public class ExportRequestDetailItemResponse
    {
        public int ExportRequestDetailId { get; set; }
        public int ProductUnitId { get; set; }
        public string SkuCode { get; set; }
        public string Name { get; set; }
        public string UnitName { get; set; }
        public int Quantity { get; set; }
    }
}
