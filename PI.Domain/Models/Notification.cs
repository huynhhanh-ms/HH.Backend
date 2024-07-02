using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("notification")]
[Index("ReceiverId", Name = "notification_account_FK")]
[Index("CreatedBy", Name = "notification_ibfk_1")]
[Index("UpdatedBy", Name = "notification_ibfk_2")]
public partial class Notification
{
    [Key]
    [Column("notification_id")]
    public int NotificationId { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("body", TypeName = "text")]
    public string Body { get; set; } = null!;

    [Column("data", TypeName = "text")]
    public string? Data { get; set; }

    [Column("image")]
    [StringLength(255)]
    public string? Image { get; set; }

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

    [Column("is_read")]
    public bool IsRead { get; set; }

    [Column("receiver_id")]
    public int ReceiverId { get; set; }

    [ForeignKey("ReceiverId")]
    [InverseProperty("Notifications")]
    public virtual Account Receiver { get; set; } = null!;
}
