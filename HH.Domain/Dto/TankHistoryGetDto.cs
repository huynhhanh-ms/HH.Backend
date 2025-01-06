using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto
{
    public class TankHistoryGetDto
    {
        public string Name { get; set; } = null!;

        public int? TypeId { get; set; }

        public decimal? Height { get; set; }

        public decimal? CurrentVolume { get; set; }

        public decimal? Capacity { get; set; }
    }

}
