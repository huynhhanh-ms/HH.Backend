using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Table("fuel_import_session")]
//[Index("FuelImportId", Name = "idx_fuel_import_session_fuel_import_id")]
//[Index("SessionId", Name = "idx_fuel_import_session_session_id")]
public partial class FuelImportSession
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fuel_import_id")]
    public int FuelImportId { get; set; }

    [Column("session_id")]
    public int SessionId { get; set; }

    [Column("volume_used")]
    public decimal? VolumeUsed { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("sale_price")]
    public decimal? SalePrice { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("FuelImportId")]
    [InverseProperty("FuelImportSessions")]
    public virtual FuelImport FuelImport { get; set; } = null!;

    [ForeignKey("SessionId")]
    [InverseProperty("FuelImportSessions")]
    public virtual Session Session { get; set; } = null!;
}
