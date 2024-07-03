using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("stock_check")]
[Index("CreatedBy", Name = "fk_created_by_id")]
[Index("StockkeeperId", Name = "fk_stockkeeper_id")]
[Index("StaffId", Name = "staff_id")]
public partial class StockCheck
{
    [Key]
    [Column("stock_check_id")]
    public int StockCheckId { get; set; }

    [Column("note", TypeName = "text")]
    public string? Note { get; set; }

    [Column("document_link")]
    [StringLength(255)]
    public string? DocumentLink { get; set; }

    [Column("staff_id")]
    public int? StaffId { get; set; }

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

    [Column("priority", TypeName = "enum('Highest','High','Medium')")]
    public string Priority { get; set; } = null!;

    [Column("status", TypeName = "enum('Todo','Assigned','Accepted','AssignmentDeclined','Draft','Submitted','Confirmed','Completed','Rejected')")]
    public string Status { get; set; } = null!;

    [Column("stockkeeper_id")]
    public int? StockkeeperId { get; set; }

    [Column("due_date", TypeName = "timestamp(6)")]
    public DateTime DueDate { get; set; }

    [Column("title")]
    [StringLength(150)]
    public string Title { get; set; } = null!;

    [Column("stock_check_type", TypeName = "enum('Regular','Spot')")]
    public string StockCheckType { get; set; } = null!;

    [Column("is_used_for_balancing")]
    public bool IsUsedForBalancing { get; set; }

    [Column("start_date", TypeName = "timestamp(6)")]
    public DateTime StartDate { get; set; }

    [Column("log", TypeName = "text")]
    public string? Log { get; set; }

    [Column("staff_signature")]
    [StringLength(255)]
    public string? StaffSignature { get; set; }

    [Column("stockkeeper_signature")]
    [StringLength(255)]
    public string? StockkeeperSignature { get; set; }

    [Column("manager_signature")]
    [StringLength(255)]
    public string? ManagerSignature { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [ForeignKey("CreatedBy")]
    [InverseProperty("StockCheckCreatedByNavigations")]
    public virtual Account CreatedByNavigation { get; set; } = null!;

    [ForeignKey("StaffId")]
    [InverseProperty("StockCheckStaffs")]
    public virtual Account? Staff { get; set; }

    [InverseProperty("StockCheck")]
    public virtual ICollection<StockCheckDetail> StockCheckDetails { get; set; } = new List<StockCheckDetail>();

    [ForeignKey("StockkeeperId")]
    [InverseProperty("StockCheckStockkeepers")]
    public virtual Account? Stockkeeper { get; set; }
}
