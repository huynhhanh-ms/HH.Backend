using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto
{
    public class FuelImportGetDto
    {
        public int TankId { get; set; }

        public TankGetDto Tank { get; set; } = null!;

        public decimal ImportVolume { get; set; }

        public decimal ImportPrice { get; set; }

        public decimal? Weight { get; set; }
    }
}
