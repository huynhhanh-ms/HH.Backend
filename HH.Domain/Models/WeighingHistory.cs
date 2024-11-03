using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Table("weighing_history")]
public partial class WeighingHistory
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("customer_name")]
    [StringLength(100)]
    public string? CustomerName { get; set; }

    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    [Column("goods_type")]
    [StringLength(200)]
    public string? GoodsType { get; set; }

    [Column("license_plate")]
    [StringLength(20)]
    public string? LicensePlate { get; set; }

    [Column("total_weight")]
    public int? TotalWeight { get; set; }

    [Column("vehicle_weight")]
    public int? VehicleWeight { get; set; }

    [Column("goods_weight")]
    public int? GoodsWeight { get; set; }

    [Column("price")]
    public decimal? Price { get; set; }

    [Column("total_cost")]
    public decimal? TotalCost { get; set; }

    [Column("total_weighing_date", TypeName = "timestamp without time zone")]
    public DateTime? TotalWeighingDate { get; set; }

    [Column("vehicle_weighing_date", TypeName = "timestamp without time zone")]
    public DateTime? VehicleWeighingDate { get; set; }

    [Column("note")]
    public string? Note { get; set; }

    [Column("vehicle_images")]
    public List<string>? VehicleImages { get; set; }

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
}
