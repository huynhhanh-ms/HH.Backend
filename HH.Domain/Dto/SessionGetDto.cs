using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto;

public class SessionGetDto
{
    public int Id { get; set; }

    public decimal? TotalRevenue { get; set; }

    public decimal? CashForChange { get; set; }
    public decimal? TotalExpense { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string? Status { get; set; }
    public string? Note { get; set; }


    public ICollection<ExpenseGetDto> Expenses { get; set; }

    public ICollection<PetrolPumpGetDto> PetrolPumps { get; set; }
}
