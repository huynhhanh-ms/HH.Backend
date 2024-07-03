using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("assignment")]
[Index("AssigneeId", Name = "assignee_id")]
[Index("ReporterId", Name = "reporter_id")]
public partial class Assignment
{
    [Key]
    [Column("assignment_id")]
    public int AssignmentId { get; set; }

    [Column("title")]
    [StringLength(150)]
    public string Title { get; set; } = null!;

    [Column("label")]
    [StringLength(100)]
    public string Label { get; set; } = null!;

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("priority", TypeName = "enum('highest','high','medium','low','lowest')")]
    public string Priority { get; set; } = null!;

    [Column("status", TypeName = "enum('todo','inprogress','done')")]
    public string Status { get; set; } = null!;

    [Column("due_date", TypeName = "timestamp(6)")]
    public DateTime? DueDate { get; set; }

    [Column("assignee_id")]
    public int? AssigneeId { get; set; }

    [Column("reporter_id")]
    public int ReporterId { get; set; }

    [Column("created_at")]
    [MaxLength(6)]
    public DateTime CreatedAt { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("updated_at")]
    [MaxLength(6)]
    public DateTime UpdatedAt { get; set; }

    [Column("updated_by")]
    public int UpdatedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("AssigneeId")]
    [InverseProperty("AssignmentAssignees")]
    public virtual Account? Assignee { get; set; }

    [ForeignKey("ReporterId")]
    [InverseProperty("AssignmentReporters")]
    public virtual Account Reporter { get; set; } = null!;
}
