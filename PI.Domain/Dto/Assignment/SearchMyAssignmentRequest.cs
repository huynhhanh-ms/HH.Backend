using PI.Domain.Common;
using PI.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Dto.Assignment
{
    public class SearchMyAssignmentRequest : SearchBaseRequest
    {
        [Required]
        public int AccountId { get; set; }

        public AssignmentRole? Role { get; set; } = null;

        public AssignmentStatus? Status { get; set; } = null;

        public string Label { get; set; } = string.Empty;
    }
}
