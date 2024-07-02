using PI.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace PI.Domain.Dto.Assignment
{
    public class CreateAssignmentRequest
    {
        [StringLength(150)]
        [Required]
        public string Title { get; set; } = null!;

        [StringLength(100)]
        [Required]
        public string Label { get; set; } = null!;

        [StringLength(255)]
        public string? Description { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public AssignmentPiority? Priority { get; set; } = AssignmentPiority.Low;

        [JsonIgnore]
        public AssignmentStatus? Status { get; set; } = AssignmentStatus.Todo;

        [Required]
        public DateTime DueDate { get; set; }

        public int? AssigneeId { get; set; } = null;

        [Required]
        public int ReporterId { get; set; }
    }

    public class CreateAssigmentRequestValidator : AbstractValidator<CreateAssignmentRequest>
    {
        public CreateAssigmentRequestValidator()
        {
            RuleFor(x => x.DueDate)
                .NotEmpty().Must(x => x >= DateTime.Now);
        }
    }
}
