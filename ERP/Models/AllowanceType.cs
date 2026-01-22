using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class AllowanceType
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } // "Indemnité de transport"
        
        [StringLength(20)]
        public string Code { get; set; } // "ITRANS"
        
        public string Description { get; set; }
        
        [StringLength(20)]
        public string CalculationType { get; set; } // "Fixed", "BasedOnDistance", "Percentage"
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal? DefaultAmount { get; set; }
        
        public bool IsTaxable { get; set; }
        public bool IsSubjectToSocialContributions { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        // Navigation
        public ICollection<EmployeeAllowance> EmployeeAllowances { get; set; }
    }
}