using System;
using System.Collections.Generic;

namespace HH.Domain.Models;

public partial class SessionDetail
{
    public int Id { get; set; }

    public int? SessionId { get; set; }

    public int? TankId { get; set; }

    public decimal StartVolume { get; set; }

    public decimal EndVolume { get; set; }

    public decimal? Revenue { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Session? Session { get; set; }

    public virtual Tank? Tank { get; set; }
}
