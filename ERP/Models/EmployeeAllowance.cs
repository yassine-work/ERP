using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class EmployeeAllowance
    {
        [Key]
        public int EmployeeAllowanceId { get; set; }

        [Required]
        public int CompensationPackageId { get; set; }
        public CompensationPackage CompensationPackage { get; set; }

        [Required]
        public int AllowanceTypeId { get; set; }
        public AllowanceType AllowanceType { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be zero or positive.")]
        public decimal Amount { get; set; }

        [Required]
        public bool IsRecurring { get; set; }

        [Required]
        public bool IsTaxable { get; set; }

        [MaxLength(50)]
        public string? Frequency { get; set; } // optional: Monthly, Quarterly, Yearly, etc.

        public DateTime EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; } // optional, can be null if ongoing
    }
}
