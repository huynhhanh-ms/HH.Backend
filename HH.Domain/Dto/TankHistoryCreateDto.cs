using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto;

public class TankHistoryCreateDto
{
    [Column("tank_id")]
    public int TankId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Precision(13, 2)]
    public decimal? CurrentVolume { get; set; }
}
