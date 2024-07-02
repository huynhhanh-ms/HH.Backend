using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PI.Domain.Dto.StockCheck
{
    public class AssignStockCheckRequest
    {
        [Required]
        public int StaffId { get; set; }    
    }
}
