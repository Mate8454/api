using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stock;
using api.Models;
namespace api.Dtos.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot exceed 10 characters.")]
        public string Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(100, ErrorMessage = "Company name cannot exceed 100 characters.")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]

        [Range(1, 100000000, ErrorMessage = "Purchase price must be between 1 and 100000000.")]
        public decimal Purchase { get; set; }
        [Required]

        [Range(0.01, 100, ErrorMessage = "Last dividend must be between 0.01 and 100.")]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Industry cannot exceed 100 characters.")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1, 5000000000000, ErrorMessage = "Market cap must be between 1 and 5000000000000.")]
        public long MarketCap { get; set; }

    }
}