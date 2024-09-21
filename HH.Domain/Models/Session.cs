using System;
using System.Collections.Generic;

namespace HH.Domain.Models;

public partial class Session
{
    public int Id { get; set; }

    public decimal? TotalRevenue { get; set; }

    public decimal? CashForChange { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public decimal? FuelPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<SessionDetail> SessionDetails { get; set; } = new List<SessionDetail>();
}
