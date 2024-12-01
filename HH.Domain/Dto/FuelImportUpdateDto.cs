using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto;

public class FuelImportUpdateDto
{
    public int Id { get; set; }

    public int? TankId { get; set; }

    [Precision(13, 2)]
    public decimal ImportVolume { get; set; }

    [Precision(13, 2)]
    public decimal ImportPrice { get; set; }

    public DateTime? ImportDate { get; set; }

    public decimal? Weight { get; set; }

    public decimal? TotalCost { get; set; }

    public DateTime? CreatedAt { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public decimal? VolumeUsed { get; set; }

    public string? Status { get; set; }

    public string? Note { get; set; }

    public virtual Tank? Tank { get; set; }
}
