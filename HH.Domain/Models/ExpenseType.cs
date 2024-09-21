using System;
using System.Collections.Generic;

namespace HH.Domain.Models;

public partial class ExpenseType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();
}
