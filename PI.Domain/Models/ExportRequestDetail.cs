using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("export_request_detail")]
[Index("ExportRequestId", Name = "export_request_id")]
[Index("ProductUnitId", Name = "product_unit_id")]
public partial class ExportRequestDetail
{
    [Key]
    [Column("export_request_detail_id")]
    public int ExportRequestDetailId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("product_unit_id")]
    public int ProductUnitId { get; set; }

    [Column("export_request_id")]
    public int ExportRequestId { get; set; }

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

    [ForeignKey("ExportRequestId")]
    [InverseProperty("ExportRequestDetails")]
    public virtual ExportRequest ExportRequest { get; set; } = null!;

    [ForeignKey("ProductUnitId")]
    [InverseProperty("ExportRequestDetails")]
    public virtual ProductUnit ProductUnit { get; set; } = null!;
}
