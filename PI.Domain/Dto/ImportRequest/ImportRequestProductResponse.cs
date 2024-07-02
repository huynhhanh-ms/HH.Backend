using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Dto.ImportRequest
{
    public class ImportRequestProductResponse
    {
        public int ProductUnitId { get; set; }
        public string ProductSku { get; set; } = null!;
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
