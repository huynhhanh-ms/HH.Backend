using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Dto.ProductStock
{
    public class CheckStock4ExportReqResponse
    {
        public int ProductUnitId { get; set; }
        public bool IsValid { get; set; } 
    }
}
