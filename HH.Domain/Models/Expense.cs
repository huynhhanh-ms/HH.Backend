using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Table("expense")]
public partial class Expense
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("session_id")]
    public int? SessionId { get; set; }

    [Column("expense_type_id")]
    public int? ExpenseTypeId { get; set; }

    [Column("amount")]
    [Precision(13, 2)]
    public decimal Amount { get; set; }

    [Column("note")]
    public string? Note { get; set; }

    [Column("expense_date", TypeName = "timestamp without time zone")]
    public DateTime? ExpenseDate { get; set; }

    [Column("debtor")]
    [StringLength(255)]
    public string? Debtor { get; set; }

    [Column("image")]
    [StringLength(255)]
    public string? Image { get; set; }

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

    [ForeignKey("ExpenseTypeId")]
    [InverseProperty("Expenses")]
    public virtual ExpenseType? ExpenseType { get; set; }

    [ForeignKey("SessionId")]
    [InverseProperty("Expenses")]
    public virtual Session? Session { get; set; }
}
