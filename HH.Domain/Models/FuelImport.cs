using System;
using System.Collections.Generic;

namespace HH.Domain.Models;

public partial class FuelImport
{
    public int Id { get; set; }

    public int? TankId { get; set; }

    public decimal ImportVolume { get; set; }

    public decimal ImportPrice { get; set; }

    public DateTime? ImportDate { get; set; }

    public decimal? Weight { get; set; }

    public decimal? TotalCost { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Tank? Tank { get; set; }
}
