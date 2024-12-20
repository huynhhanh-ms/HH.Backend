﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Table("session")]
public partial class Session
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("total_revenue")]
    [Precision(15, 2)]
    public decimal? TotalRevenue { get; set; }

    [Column("cash_for_change")]
    [Precision(13, 2)]
    public decimal? CashForChange { get; set; }

    [Column("start_date", TypeName = "timestamp without time zone")]
    public DateTime? StartDate { get; set; }

    [Column("end_date", TypeName = "timestamp without time zone")]
    public DateTime? EndDate { get; set; }

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

    [Column("status")]
    [StringLength(50)]
    public string? Status { get; set; }

    [Column("total_expense")]
    [Precision(15, 2)]
    public decimal? TotalExpense { get; set; }

    [Column("note")]
    public string? Note { get; set; }

    [InverseProperty("Session")]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    [InverseProperty("Session")]
    public virtual ICollection<FuelImportSession> FuelImportSessions { get; set; } = new List<FuelImportSession>();

    [InverseProperty("Session")]
    public virtual ICollection<PetrolPump> PetrolPumps { get; set; } = new List<PetrolPump>();
}
