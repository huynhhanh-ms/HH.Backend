using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("product_unit")]
[Index("UnitId", Name = "product_unit_ibfk_1")]
[Index("ProductId", Name = "product_unit_ibfk_2")]
[Index("ParentId", Name = "product_unit_product_unit_FK")]
[Index("SkuCode", Name = "sku_code_UNIQUE", IsUnique = true)]
public partial class ProductUnit
{
    [Key]
    [Column("product_unit_id")]
    public int ProductUnitId { get; set; }

    [Column("sku_code")]
    [StringLength(25)]
    public string SkuCode { get; set; } = null!;

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("is_available")]
    public bool IsAvailable { get; set; }

    [Column("unit_id")]
    public int UnitId { get; set; }

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

    [Column("conversion_value")]
    public int? ConversionValue { get; set; }

    [Column("created_at")]
    [MaxLength(6)]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    [MaxLength(6)]
    public DateTime UpdatedAt { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("updated_by")]
    public int UpdatedBy { get; set; }

    [InverseProperty("ProductUnit")]
    public virtual ICollection<ExportRequestDetail> ExportRequestDetails { get; set; } = new List<ExportRequestDetail>();

    [InverseProperty("ProductUnit")]
    public virtual ICollection<ImportRequestDetail> ImportRequestDetails { get; set; } = new List<ImportRequestDetail>();

    [InverseProperty("Parent")]
    public virtual ICollection<ProductUnit> InverseParent { get; set; } = new List<ProductUnit>();

    [InverseProperty("ProductUnit")]
    public virtual ICollection<Lot> Lots { get; set; } = new List<Lot>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual ProductUnit? Parent { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("ProductUnits")]
    public virtual Product Product { get; set; } = null!;

    [InverseProperty("ProductUnit")]
    public virtual ICollection<ProductStock> ProductStocks { get; set; } = new List<ProductStock>();

    [InverseProperty("ProductUnit")]
    public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; } = new List<ShipmentDetail>();

    [InverseProperty("ProductUnit")]
    public virtual ICollection<StockCheckDetail> StockCheckDetails { get; set; } = new List<StockCheckDetail>();

    [ForeignKey("UnitId")]
    [InverseProperty("ProductUnits")]
    public virtual Unit Unit { get; set; } = null!;
}
