using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto;

public class SessionCreateDto
{
    //[Column("total_revenue")]
    //[Precision(15, 2)]
    //public decimal? TotalRevenue { get; set; }

    [Column("cash_for_change")]
    [Precision(13, 2)]
    public decimal? CashForChange { get; set; }

    //[InverseProperty("Session")]
    //public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    //[InverseProperty("Session")]
    public ICollection<PetrolPumpCreateDto>? PetrolPumps { get; set; }
}
