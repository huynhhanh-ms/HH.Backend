using System.ComponentModel.DataAnnotations;

namespace PI.Domain.Dto.Assignment
{
    public class AssignAssignmentRequest
    {
        [Required]
        public int AssignmentId { get; set; }

        public int? AssigneeId { get; set; } = null;
    }
}
