using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class SalaryScaleLevel
    {
        public int Id { get; set; }
        
        public int SalaryScaleId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LevelName { get; set; } // "Junior", "Confirmé", "Senior"
        
        public int LevelOrder { get; set; } // 1, 2, 3...
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal MinSalary { get; set; }
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal MaxSalary { get; set; }
        
        public string Description { get; set; }
        
        // Navigation
        public SalaryScale SalaryScale { get; set; }
    }
}