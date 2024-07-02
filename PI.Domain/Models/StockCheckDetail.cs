using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("stock_check_detail")]
[Index("ProductUnitId", Name = "product_unit_id")]
[Index("StockCheckId", Name = "stock_check_id")]
public partial class StockCheckDetail
{
    [Key]
    [Column("stock_check_detail_id")]
    public int StockCheckDetailId { get; set; }

    [Column("stock_check_id")]
    public int StockCheckId { get; set; }

    [Column("product_unit_id")]
    public int ProductUnitId { get; set; }

    [Column("estimated_quantity")]
    public int? EstimatedQuantity { get; set; }

    [Column("actual_quantity")]
    public int? ActualQuantity { get; set; }

    [Column("note", TypeName = "text")]
    public string? Note { get; set; }

    [Column("created_at")]
    [MaxLength(6)]
    public DateTime CreatedAt { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("updated_at")]
    [MaxLength(6)]
    public DateTime UpdatedAt { get; set; }

    [Column("updated_by")]
    public int UpdatedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("status", TypeName = "enum('Todo','Submitted', 'Confirmed', 'Rejected')")]
    public string Status { get; set; } = null!;

    [ForeignKey("ProductUnitId")]
    [InverseProperty("StockCheckDetails")]
    public virtual ProductUnit ProductUnit { get; set; } = null!;

    [ForeignKey("StockCheckId")]
    [InverseProperty("StockCheckDetails")]
    public virtual StockCheck StockCheck { get; set; } = null!;
}
