using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("shipment")]
[Index("ExportRequestId", Name = "shipment_export_request_FK")]
[Index("DistributorId", Name = "shipment_ibfk_3")]
public partial class Shipment
{
    [Key]
    [Column("shipment_id")]
    public int ShipmentId { get; set; }

    [Column("total_unit_product_quantity")]
    public long? TotalUnitProductQuantity { get; set; }

    [Column("total_price")]
    public long? TotalPrice { get; set; }

    [Column("invoice_url")]
    [StringLength(255)]
    public string? InvoiceUrl { get; set; }

    [Column("signature_url")]
    [StringLength(255)]
    public string? SignatureUrl { get; set; }

    [Column("shipment_date")]
    [MaxLength(6)]
    public DateTime? ShipmentDate { get; set; }

    [Column("shipment_type", TypeName = "enum('Import','Export','ImportBalance','ExportBalance')")]
    public string? ShipmentType { get; set; }

    [Column("distributor_id")]
    public int? DistributorId { get; set; }

    [Column("created_at")]
    [MaxLength(6)]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    [MaxLength(6)]
    public DateTime UpdatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("export_request_id")]
    public int? ExportRequestId { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("updated_by")]
    public int UpdatedBy { get; set; }

    [Column("import_request_id")]
    public int? ImportRequestId { get; set; }

    [Column("storekeeper_url")]
    [StringLength(255)]
    public string? StorekeeperUrl { get; set; }

    [Column("distributor_url")]
    [StringLength(255)]
    public string? DistributorUrl { get; set; }

    [Column("receipt_url")]
    [StringLength(255)]
    public string? ReceiptUrl { get; set; }

    [ForeignKey("DistributorId")]
    [InverseProperty("Shipments")]
    public virtual Distributor? Distributor { get; set; }

    [ForeignKey("ExportRequestId")]
    [InverseProperty("Shipments")]
    public virtual ExportRequest? ExportRequest { get; set; }

    [InverseProperty("Shipment")]
    public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; } = new List<ShipmentDetail>();
}
