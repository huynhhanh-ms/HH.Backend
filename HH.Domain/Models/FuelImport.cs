using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Table("fuel_import")]
public partial class FuelImport
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("tank_id")]
    public int? TankId { get; set; }

    [Column("import_volume")]
    [Precision(13, 2)]
    public decimal ImportVolume { get; set; }

    [Column("import_price")]
    [Precision(13, 2)]
    public decimal ImportPrice { get; set; }

    [Column("import_date", TypeName = "timestamp without time zone")]
    public DateTime? ImportDate { get; set; }

    [Column("weight")]
    [Precision(13, 2)]
    public decimal? Weight { get; set; }

    [Column("total_cost")]
    [Precision(15, 2)]
    public decimal? TotalCost { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("TankId")]
    [InverseProperty("FuelImports")]
    public virtual Tank? Tank { get; set; }
}
