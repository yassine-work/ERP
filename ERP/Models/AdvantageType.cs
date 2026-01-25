using System;
using System.ComponentModel.DataAnnotations;
namespace ERP.Models
{  
    public class AdvantageType
    {
        [Key]
        public int AdvantageTypeId { get; set; }

        [Required(ErrorMessage = "Le nom de l'avantage est obligatoire")]
        public string AdvantageTypeName { get; set; } // "Insurance", "Transport", "Phone"

        [MaxLength(100)]
        public string? Provider { get; set; } // optional, e.g., insurance company

        [MaxLength(500)]
        public string? EligibilityRule { get; set; } // optional, conditions or notes
    }
}