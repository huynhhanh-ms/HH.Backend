using HH.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HH.Domain.Dto;

public class FuelImportCreateDto
{
    [Required(ErrorMessage = "Phải chọn bồn nhập")]
    public int TankId { get; set; }

    [Required]
    [Range(1, 100000, ErrorMessage = "Số lít nhập phải lớn hơn 0")]
    public decimal ImportVolume { get; set; }

    [Required]
    [Range(1, 100000, ErrorMessage = "Giá nhập phải lớn hơn 0")]
    public decimal ImportPrice { get; set; }

    [Range(1, 100000, ErrorMessage = "Tổng tiền phải lớn hơn 0")]
    public decimal? Weight { get; set; }
}
