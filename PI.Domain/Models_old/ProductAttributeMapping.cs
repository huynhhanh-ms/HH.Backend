using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("product_attribute_mapping")]
[Index("ProductAttributeId", Name = "product_attribute_mapping_ibfk_1")]
[Index("ProductId", Name = "product_attribute_mapping_ibfk_2")]
public partial class ProductAttributeMapping
{
    [Key]
    [Column("product_at_mp_id")]
    public int ProductAtMpId { get; set; }

    [Column("value")]
    [StringLength(255)]
    public string Value { get; set; } = null!;

    [Column("product_id")]
    public int ProductId { get; set; }

    [Column("product_attribute_id")]
    public int ProductAttributeId { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

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

    [ForeignKey("ProductId")]
    [InverseProperty("ProductAttributeMappings")]
    public virtual Product Product { get; set; } = null!;

    [ForeignKey("ProductAttributeId")]
    [InverseProperty("ProductAttributeMappings")]
    public virtual ProductAttribute ProductAttribute { get; set; } = null!;
}
