using System;
using System.Collections.Generic;

namespace HH.Domain.Models;

public partial class Lubricant
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CurrentStock { get; set; }

    public decimal? ImportPrice { get; set; }

    public decimal? SellPrice { get; set; }

    public DateTime? ImportDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }
}
