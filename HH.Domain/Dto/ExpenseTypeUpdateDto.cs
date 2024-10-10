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
    public class ExpenseTypeUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

    }
}
