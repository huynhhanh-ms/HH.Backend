using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Table("fuel_price")]
public partial class FuelPrice
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("fuel_type_id")]
    public int? FuelTypeId { get; set; }

    [Column("selling_price")]
    [Precision(13, 2)]
    public decimal SellingPrice { get; set; }

    [Column("import_price")]
    [Precision(13, 2)]
    public decimal ImportPrice { get; set; }

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

    [ForeignKey("FuelTypeId")]
    [InverseProperty("FuelPrices")]
    public virtual FuelType? FuelType { get; set; }
}
