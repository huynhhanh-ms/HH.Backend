using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto.WeighingHistory
{
    public class WeighingHistoryUpdateDto
    {
        public int Id { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? GoodsType { get; set; }
        public string? LicensePlate { get; set; }
        public int? TotalWeight { get; set; }
        public int? VehicleWeight { get; set; }
        public int? GoodsWeight { get; set; }
        public DateTime? TotalWeighingDate { get; set; }
        public DateTime? VehicleWeighingDate { get; set; }
        public string? Note { get; set; }
        public List<string>? VehicleImages { get; set; }
    }
}
