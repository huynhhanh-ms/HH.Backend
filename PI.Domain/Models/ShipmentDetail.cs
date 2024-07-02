using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("shipment_detail")]
[Index("ProductUnitId", Name = "shipment_detail_ibfk_1")]
[Index("LotId", Name = "shipment_detail_ibfk_2")]
[Index("ShipmentId", Name = "shipment_detail_ibfk_3")]
public partial class ShipmentDetail
{
    [Key]
    [Column("shipment_detail_id")]
    public int ShipmentDetailId { get; set; }

    [Column("quantity")]
    public int Quantity { get; set; }

    [Column("cost")]
    public long? Cost { get; set; }

    [Column("price")]
    public long? Price { get; set; }

    [Column("shipment_id")]
    public int ShipmentId { get; set; }

    [Column("lot_id")]
    public int? LotId { get; set; }

    [Column("product_unit_id")]
    public int ProductUnitId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("created_at", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("updated_at", TypeName = "datetime")]
    public DateTime UpdatedAt { get; set; }

    [Column("updated_by")]
    public int UpdatedBy { get; set; }

    [ForeignKey("LotId")]
    [InverseProperty("ShipmentDetails")]
    public virtual Lot? Lot { get; set; }

    [ForeignKey("ProductUnitId")]
    [InverseProperty("ShipmentDetails")]
    public virtual ProductUnit ProductUnit { get; set; } = null!;

    [ForeignKey("ShipmentId")]
    [InverseProperty("ShipmentDetails")]
    public virtual Shipment Shipment { get; set; } = null!;
}
