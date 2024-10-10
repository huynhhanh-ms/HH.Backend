using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HH.Domain.Models;

namespace HH.Domain.Dto
{
    public class ExpenseUpdateDto
    {
        public int Id { get; set; }

        public int? SessionId { get; set; }

        public int? ExpenseTypeId { get; set; }

        public decimal Amount { get; set; }

        public string? Note { get; set; }

        public string? Debtor { get; set; }

        public string? Image { get; set; }
    }
}
