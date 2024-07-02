using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PI.Domain.Models;

[Table("medicine")]
[Index("ManufacturerId", Name = "medicine_ibfk_2")]
[Index("UnitId", Name = "medicine_unit_FK")]
public partial class Medicine
{
    [Key]
    [Column("medicine_id")]
    public int MedicineId { get; set; }

    [Column("registration_no")]
    [StringLength(20)]
    public string RegistrationNo { get; set; } = null!;

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("indication", TypeName = "text")]
    public string? Indication { get; set; }

    [Column("manufacturer_id")]
    public int? ManufacturerId { get; set; }

    [Column("packing_size")]
    [StringLength(255)]
    public string? PackingSize { get; set; }

    [Column("medicinecol")]
    [StringLength(45)]
    public string? Medicinecol { get; set; }

    [Column("is_deleted")]
    public bool IsDeleted { get; set; }

    [Column("unit_id")]
    public int UnitId { get; set; }

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

    [ForeignKey("ManufacturerId")]
    [InverseProperty("Medicines")]
    public virtual Manufacturer? Manufacturer { get; set; }

    [InverseProperty("Medicine")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [ForeignKey("UnitId")]
    [InverseProperty("Medicines")]
    public virtual Unit Unit { get; set; } = null!;

    [ForeignKey("MedicineId")]
    [InverseProperty("Medicines")]
    public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
}
