using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("category_setting")]
[Index("CategoryId", Name = "category_setting_ibfk_1")]
public partial class CategorySetting
{
    [Key]
    [Column("category_setting_id")]
    public int CategorySettingId { get; set; }

    [Column("max_quantity")]
    public int MaxQuantity { get; set; }

    [Column("min_quantity")]
    public int MinQuantity { get; set; }

    [Column("category_id")]
    public int? CategoryId { get; set; }

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

    [ForeignKey("CategoryId")]
    [InverseProperty("CategorySettings")]
    public virtual Category? Category { get; set; }
}
