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
        public decimal? Price { get; set; }
        public decimal? TotalCost { get; set; }
        public DateTimeOffset? TotalWeighingDate { get; set; }
        public DateTimeOffset? VehicleWeighingDate { get; set; }
        public string? Note { get; set; }
        public List<string>? VehicleImages { get; set; }
        public DateTime? CreatedAt { get; set; }

    }
}
