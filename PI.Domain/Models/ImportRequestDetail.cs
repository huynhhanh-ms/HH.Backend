using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("import_request_detail")]
[Index("ImportRequestId", Name = "import_request_id")]
[Index("ProductUnitId", Name = "product_unit_id")]
public partial class ImportRequestDetail
{
    [Key]
    [Column("import_request_detail_id")]
    public int ImportRequestDetailId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("product_unit_id")]
    public int ProductUnitId { get; set; }

    [Column("import_request_id")]
    public int ImportRequestId { get; set; }

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

    [ForeignKey("ImportRequestId")]
    [InverseProperty("ImportRequestDetails")]
    public virtual ImportRequest ImportRequest { get; set; } = null!;

    [ForeignKey("ProductUnitId")]
    [InverseProperty("ImportRequestDetails")]
    public virtual ProductUnit ProductUnit { get; set; } = null!;
}
