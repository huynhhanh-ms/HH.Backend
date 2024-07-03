using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("account")]
[Index("Username", Name = "username_UNIQUE", IsUnique = true)]
public partial class Account
{
    [Key]
    [Column("account_id")]
    public int AccountId { get; set; }

    [Column("username")]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Column("fullname")]
    [StringLength(100)]
    public string Fullname { get; set; } = null!;

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("password_hash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("role", TypeName = "enum('Staff','Manager','Stockkeeper')")]
    public string Role { get; set; } = null!;

    [Column("status", TypeName = "enum('Active','Inactive')")]
    public string Status { get; set; } = null!;

    [Column("phone")]
    [StringLength(12)]
    public string? Phone { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("created_at")]
    [MaxLength(6)]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    [MaxLength(6)]
    public DateTime UpdatedAt { get; set; }

    [Column("created_by")]
    public int CreatedBy { get; set; }

    [Column("updated_by")]
    public int UpdatedBy { get; set; }

    [InverseProperty("Receiver")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<Product> ProductCreatedByNavigations { get; set; } = new List<Product>();

    [InverseProperty("UpdatedByNavigation")]
    public virtual ICollection<Product> ProductUpdatedByNavigations { get; set; } = new List<Product>();

    [InverseProperty("CreatedByNavigation")]
    public virtual ICollection<StockCheck> StockCheckCreatedByNavigations { get; set; } = new List<StockCheck>();

    [InverseProperty("Staff")]
    public virtual ICollection<StockCheck> StockCheckStaffs { get; set; } = new List<StockCheck>();

    [InverseProperty("Stockkeeper")]
    public virtual ICollection<StockCheck> StockCheckStockkeepers { get; set; } = new List<StockCheck>();
}
