using PI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PI.Domain.Enums.StockCheckEnum;

namespace PI.Domain.Dto.StockCheck
{
    public class SearchStockCheckRequest : SearchBaseRequest
    {
        public DateTime? DateFrom { get; set; } = null;

        public DateTime? DateTo { get; set; } = null;

        public StockCheckPriority[]? Priority { get; set; } = null;

        public StockCheckStatus[]? Status { get; set; } = null;

        public int? StaffId { get; set; } = null;

        public int? StockkeeperId { get; set; } = null;

        public bool? IsUsedForBalancing { get; set; } = null;
    }
}
