using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("product_stock")]
[Index("ProductUnitId", Name = "product_unit_id")]
public partial class ProductStock
{
    [Key]
    [Column("product_stock_id")]
    public int ProductStockId { get; set; }

    [Column("stock_quantity")]
    public long StockQuantity { get; set; }

    [Column("product_unit_id")]
    public int ProductUnitId { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [Column("updated_by")]
    public int UpdatedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Required]
    [Column("is_current")]
    public bool? IsCurrent { get; set; }

    [ForeignKey("ProductUnitId")]
    [InverseProperty("ProductStocks")]
    public virtual ProductUnit ProductUnit { get; set; } = null!;
}
