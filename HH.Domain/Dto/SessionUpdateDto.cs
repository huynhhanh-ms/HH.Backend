using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto;

public class SessionUpdateDto
{
    public int Id { get; set; }

    public decimal? TotalRevenue { get; set; }

    public decimal? CashForChange { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }
    //[InverseProperty("Session")]
    //public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    //[InverseProperty("Session")]
    public ICollection<PetrolPumpUpdateDto> PetrolPumps { get; set; }
}
