using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class EmployeeBenefit
    {
        public int Id { get; set; }
        
        public int EmployeeId { get; set; }
        public int BenefitTypeId { get; set; }
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal? CustomValue { get; set; } // Si différent de la valeur standard
        
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        [StringLength(100)]
        public string SerialNumber { get; set; } // Pour véhicule, laptop, etc.
        
        [StringLength(20)]
        public string Status { get; set; } // "Active", "PendingApproval", "Ended"
        
        public string Notes { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedAt { get; set; }
        
        // Navigation
        public Employe Employee { get; set; }
        public BenefitType BenefitType { get; set; }
    }
    
}