using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto
{
    public class PetrolPumpCreateDto
    {
        [Column("tank_id")]
        public int? TankId { get; set; }

        [Column("start_volume")]
        [Precision(13, 2)]
        public decimal StartVolume { get; set; }

        //[Column("end_volume")]
        //[Precision(13, 2)]
        //public decimal EndVolume { get; set; }
    }
}
