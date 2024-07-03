using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("lot")]
[Index("ProductUnitId", Name = "lot_ibfk_1")]
public partial class Lot
{
    [Key]
    [Column("lot_id")]
    public int LotId { get; set; }

    [Column("lot_code")]
    [StringLength(25)]
    public string LotCode { get; set; } = null!;

    [Column("manufacturing_date")]
    [MaxLength(6)]
    public DateTime ManufacturingDate { get; set; }

    [Column("expiration_date")]
    [MaxLength(6)]
    public DateTime ExpirationDate { get; set; }

    [Column("lot_status", TypeName = "enum('ACTIVE','EXPIRED','SOLDOUT')")]
    public string LotStatus { get; set; } = null!;

    [Column("product_unit_id")]
    public int ProductUnitId { get; set; }

    [Column("created_at")]
    [MaxLength(6)]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    [MaxLength(6)]
    public DateTime UpdatedAt { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("updated_by")]
    public int UpdatedBy { get; set; }

    [ForeignKey("ProductUnitId")]
    [InverseProperty("Lots")]
    public virtual ProductUnit ProductUnit { get; set; } = null!;

    [InverseProperty("Lot")]
    public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; } = new List<ShipmentDetail>();
}
