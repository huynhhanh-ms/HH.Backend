using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace HH.Domain.Models;

[Table("tank")]
public partial class Tank
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("type_id")]
    public int? TypeId { get; set; }

    [Column("height")]
    [Precision(13, 2)]
    public decimal? Height { get; set; }

    [Column("current_volume")]
    [Precision(13, 2)]
    public decimal? CurrentVolume { get; set; }

    [Column("capacity")]
    [Precision(13, 2)]
    public decimal? Capacity { get; set; }

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

    [JsonIgnore]
    [InverseProperty("Tank")]
    public virtual ICollection<FuelImport> FuelImports { get; set; } = new List<FuelImport>();

    [InverseProperty("Tank")]
    public virtual ICollection<PetrolPump> PetrolPumps { get; set; } = new List<PetrolPump>();

    [ForeignKey("TypeId")]
    [InverseProperty("Tanks")]
    public virtual FuelType? Type { get; set; }
}
