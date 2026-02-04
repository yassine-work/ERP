using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class EmployeeBonus
    {
        [Key]
        public int EmployeeBonusId { get; set; }

        [Required]
        public int CompensationPackageId { get; set; }
        public CompensationPackage CompensationPackage { get; set; }

        [Required]
        public int BonusTypeId { get; set; }
        public BonusType BonusType { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be zero or positive.")]
        public decimal Amount { get; set; }

        [Required]
        public bool IsAutomatic { get; set; } = false;

        [Required]
        public bool IsTaxable { get; set; } = true;

        [Required]
        public bool IsExceptional { get; set; } = false;

        [Required]
        public bool IsPerformanceBased { get; set; } = false;

        [MaxLength(500)]
        public string? BonusRule { get; set; } // optional, conditions or formula

        public DateTime AwardedOn { get; set; } = DateTime.Now;
        public DateTime? ValidUntil { get; set; } // optional, can be null if ongoing
    }
}
