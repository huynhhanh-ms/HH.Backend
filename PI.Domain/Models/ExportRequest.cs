using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("export_request")]
public partial class ExportRequest
{
    [Key]
    [Column("export_request_id")]
    public int ExportRequestId { get; set; }

    [Column("export_status", TypeName = "enum('pending','processing','completed','rejected')")]
    public string? ExportStatus { get; set; }

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

    [InverseProperty("ExportRequest")]
    public virtual ICollection<ExportRequestDetail> ExportRequestDetails { get; set; } = new List<ExportRequestDetail>();

    [InverseProperty("ExportRequest")]
    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
