﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Table("lubricant")]
public partial class Lubricant
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("current_stock")]
    public int CurrentStock { get; set; }

    [Column("import_price")]
    [Precision(13, 2)]
    public decimal? ImportPrice { get; set; }

    [Column("sell_price")]
    [Precision(13, 2)]
    public decimal? SellPrice { get; set; }

    [Column("import_date", TypeName = "timestamp without time zone")]
    public DateTime? ImportDate { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }
}
