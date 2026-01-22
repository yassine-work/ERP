using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class SalaryScale
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } // "Grille Ingénieurs 2026"
        
        [StringLength(50)]
        public string Category { get; set; } // "Cadres", "Non-cadres"
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal MinSalary { get; set; }
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal MaxSalary { get; set; }
        
        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        // Navigation
        public ICollection<SalaryScaleLevel> Levels { get; set; }
    }
}