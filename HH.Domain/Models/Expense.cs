using System;
using System.Collections.Generic;

namespace HH.Domain.Models;

public partial class Expense
{
    public int Id { get; set; }

    public int? SessionId { get; set; }

    public int? ExpenseTypeId { get; set; }

    public decimal Amount { get; set; }

    public string? Note { get; set; }

    public DateTime? ExpenseDate { get; set; }

    public string? Debtor { get; set; }

    public string? Image { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ExpenseType? ExpenseType { get; set; }

    public virtual Session? Session { get; set; }
}
