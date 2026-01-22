using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class PayrollParameter
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string ParameterName { get; set; } // "CNSS_Rate_Employee"
        
        [StringLength(50)]
        public string Category { get; set; } // "SocialContribution", "Tax", "General"
        
        public decimal? NumericValue { get; set; }
        public string TextValue { get; set; }
        
        public string Description { get; set; }
        
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    
}