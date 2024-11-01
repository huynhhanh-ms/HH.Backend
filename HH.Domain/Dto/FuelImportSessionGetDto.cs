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
    public class FuelImportSessionGetDto
    {
        public int Id { get; set; }

        public int FuelImportId { get; set; }

        public int SessionId { get; set; }

        public decimal? VolumeUsed { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public decimal? SalePrice { get; set; }
    }
}
