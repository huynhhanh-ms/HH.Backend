using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto;

public class TankCreateDto
{
    [StringLength(100)]
    public string Name { get; set; } = null!;

    public int? TypeId { get; set; }

    [Precision(13, 2)]
    public decimal? Height { get; set; }

    [Precision(13, 2)]
    public decimal? CurrentVolume { get; set; }

    [Precision(13, 2)]
    public decimal? Capacity { get; set; }
}
