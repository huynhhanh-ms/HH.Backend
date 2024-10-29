using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto
{
    public class FuelImportGetDto
    {
        public int Id { get; set; }
        public int TankId { get; set; }

        public TankGetDto Tank { get; set; } = null!;

        public decimal ImportVolume { get; set; }

        public decimal ImportPrice { get; set; }

        public decimal? Weight { get; set; }


        public DateTime? ImportDate { get; set; }

        public decimal? TotalCost { get; set; }


        public decimal? VolumeUsed { get; set; }

        public string? Status { get; set; }

        public decimal? TotalSalePrice { get; set; }

        public ICollection<FuelImportSession> FuelImportSessions { get; set; } = new List<FuelImportSession>();
    }
}
