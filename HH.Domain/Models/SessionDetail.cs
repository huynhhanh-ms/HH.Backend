using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Table("session_detail")]
public partial class SessionDetail
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("session_id")]
    public int? SessionId { get; set; }

    [Column("tank_id")]
    public int? TankId { get; set; }

    [Column("start_volume")]
    [Precision(13, 2)]
    public decimal StartVolume { get; set; }

    [Column("end_volume")]
    [Precision(13, 2)]
    public decimal EndVolume { get; set; }

    [Column("revenue")]
    [Precision(13, 2)]
    public decimal? Revenue { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime? CreatedAt { get; set; }

    [Column("created_by")]
    public int? CreatedBy { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [ForeignKey("SessionId")]
    [InverseProperty("SessionDetails")]
    public virtual Session? Session { get; set; }

    [ForeignKey("TankId")]
    [InverseProperty("SessionDetails")]
    public virtual Tank? Tank { get; set; }
}
