using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class BonusType
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } // "Prime d'ancienneté"
        
        [StringLength(20)]
        public string Code { get; set; } // "PANC"
        
        public string Description { get; set; }
        
        [Required]
        [StringLength(20)]
        public string CalculationType { get; set; } // "Fixed", "Percentage", "Formula"
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal? DefaultAmount { get; set; }
        
        public decimal? DefaultPercentage { get; set; }
        
        [StringLength(20)]
        public string Frequency { get; set; } // "Monthly", "Quarterly", "Annual", "OneTime"
        
        public bool IsTaxable { get; set; }
        public bool IsSubjectToSocialContributions { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        // Navigation
        public ICollection<EmployeeBonus> EmployeeBonuses { get; set; }
    }
}