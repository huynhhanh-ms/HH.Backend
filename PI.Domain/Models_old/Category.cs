using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("category")]
[Index("ParentId", Name = "parent_id")]
public partial class Category
{
    [Key]
    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("has_children")]
    public bool HasChildren { get; set; }

    [Column("parent_id")]
    public int? ParentId { get; set; }

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

    [InverseProperty("Category")]
    public virtual ICollection<CategorySetting> CategorySettings { get; set; } = new List<CategorySetting>();

    [InverseProperty("Parent")]
    public virtual ICollection<Category> InverseParent { get; set; } = new List<Category>();

    [ForeignKey("ParentId")]
    [InverseProperty("InverseParent")]
    public virtual Category? Parent { get; set; }

    [InverseProperty("Category")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
