using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("product")]
[Index("ManufacturerId", Name = "product_ibfk_1")]
[Index("CategoryId", Name = "product_ibfk_2")]
[Index("CreatedBy", Name = "product_ibfk_3")]
[Index("UpdatedBy", Name = "product_ibfk_4")]
[Index("MedicineId", Name = "product_ibfk_5")]
public partial class Product
{
    [Key]
    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("packing_size")]
    [StringLength(255)]
    public string? PackingSize { get; set; }

    [Column("net_weight")]
    public float? NetWeight { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("manufacturer_id")]
    public int? ManufacturerId { get; set; }

    [Column("medicine_id")]
    public int? MedicineId { get; set; }

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

    [Column("is_available")]
    public bool IsAvailable { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category Category { get; set; } = null!;

    [ForeignKey("CreatedBy")]
    [InverseProperty("ProductCreatedByNavigations")]
    public virtual Account CreatedByNavigation { get; set; } = null!;

    [ForeignKey("ManufacturerId")]
    [InverseProperty("Products")]
    public virtual Manufacturer? Manufacturer { get; set; }

    [ForeignKey("MedicineId")]
    [InverseProperty("Products")]
    public virtual Medicine? Medicine { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; } = new List<ProductAttributeMapping>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    [InverseProperty("Product")]
    public virtual ICollection<ProductUnit> ProductUnits { get; set; } = new List<ProductUnit>();

    [ForeignKey("UpdatedBy")]
    [InverseProperty("ProductUpdatedByNavigations")]
    public virtual Account UpdatedByNavigation { get; set; } = null!;
}
