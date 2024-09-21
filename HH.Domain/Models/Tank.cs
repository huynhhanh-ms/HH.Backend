using System;
using System.Collections.Generic;

namespace HH.Domain.Models;

public partial class Tank
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? TypeId { get; set; }

    public decimal? Height { get; set; }

    public decimal? CurrentVolume { get; set; }

    public decimal? Capacity { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<FuelImport> FuelImports { get; set; } = new List<FuelImport>();

    public virtual ICollection<SessionDetail> SessionDetails { get; set; } = new List<SessionDetail>();

    public virtual ProductType? Type { get; set; }
}
