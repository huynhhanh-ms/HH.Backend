using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HH.Domain.Dto
{
    public class PetrolPumpUpdateDto
    {
        public int Id { get; set; }

        public int? SessionId { get; set; }

        public int? TankId { get; set; }

        public decimal StartVolume { get; set; }

        public decimal EndVolume { get; set; }

        public decimal? Revenue { get; set; }

        public int? TotalVolume { get; set; }

        public int? Price { get; set; }
    }
}
