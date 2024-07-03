using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("product_attribute")]
public partial class ProductAttribute
{
    [Key]
    [Column("product_attribute_id")]
    public int ProductAttributeId { get; set; }

    [Column("product_att_name")]
    [StringLength(255)]
    public string ProductAttName { get; set; } = null!;

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

    [InverseProperty("ProductAttribute")]
    public virtual ICollection<ProductAttributeMapping> ProductAttributeMappings { get; set; } = new List<ProductAttributeMapping>();
}
