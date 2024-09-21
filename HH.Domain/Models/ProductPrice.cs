using System;
using System.Collections.Generic;

namespace HH.Domain.Models;

public partial class ProductPrice
{
    public int Id { get; set; }

    public int? ProductTypeId { get; set; }

    public decimal SellingPrice { get; set; }

    public decimal ImportPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int UpdatedBy { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ProductType? ProductType { get; set; }
}
