using PI.Domain.Common.Entity;
using System;
using System.Collections.Generic;

namespace PI.Domain.Models;

public partial class WeighingHistory : IEntity
{
    public int WhId { get; set; }

    public string? CustomerName { get; set; }

    public string? Address { get; set; }

    public string? GoodsType { get; set; }

    public string? LicensePlate { get; set; }

    public int? TotalWeight { get; set; }

    public int? VehicleWeight { get; set; }

    public int? GoodsWeight { get; set; }

    public DateTime? TotalWeighingDate { get; set; }

    public DateTime? VehicleWeighingDate { get; set; }

    public string? Note { get; set; }

    public List<string>? VehicleImages { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    int IAuditable.CreatedBy { get; set; }
    int IAuditable.UpdatedBy { get; set; }
}
