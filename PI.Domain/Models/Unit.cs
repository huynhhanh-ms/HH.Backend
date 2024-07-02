using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("unit")]
public partial class Unit
{
    [Key]
    [Column("unit_id")]
    public int UnitId { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("updated_by")]
    public int UpdatedBy { get; set; }

    [Column("updated_at")]
    [MaxLength(6)]
    public DateTime UpdatedAt { get; set; }

    [Column("created_at")]
    [MaxLength(6)]
    public DateTime CreatedAt { get; set; }

    [InverseProperty("Unit")]
    public virtual ICollection<Medicine> Medicines { get; set; } = new List<Medicine>();

    [InverseProperty("Unit")]
    public virtual ICollection<ProductUnit> ProductUnits { get; set; } = new List<ProductUnit>();
}
