using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class CompensationPackage
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } // "Package Junior", "Package Cadre"
        
        public string Description { get; set; }
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal BaseSalary { get; set; }
        
        // JSON string contenant IDs des primes/indemnités/avantages inclus
        public string IncludedBonuses { get; set; }
        public string IncludedAllowances { get; set; }
        public string IncludedBenefits { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
    
}