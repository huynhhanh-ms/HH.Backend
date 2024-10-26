using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Keyless]
[Table("trigger_log")]
public partial class TriggerLog
{
    [Column("create_at", TypeName = "timestamp without time zone")]
    public DateTime CreateAt { get; set; }

    [Column("id")]
    public int Id { get; set; }

    [Column("description")]
    [StringLength(255)]
    public string? Description { get; set; }

    [Column("counter")]
    public int? Counter { get; set; }
}
