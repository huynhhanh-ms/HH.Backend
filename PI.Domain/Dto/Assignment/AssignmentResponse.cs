using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PI.Domain.Dto.Account;

namespace PI.Domain.Dto.Assignment
{
    public class AssignmentResponse
    {
        public int AssignmentId { get; set; }

        public string Title { get; set; } = null!;

        public string Lable { get; set; } = null!;

        public string? Description { get; set; }

        public string Priority { get; set; } = null!;

        public string Status { get; set; } = null!;

        public DateTime? DueDate { get; set; }

        public int? AssigneeId { get; set; }

        public int ReporterId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int UpdatedBy { get; set; }

        public AccountResponse? Assignee { get; set; }

        public AccountResponse Reporter { get; set; } = null!;
    }
}
