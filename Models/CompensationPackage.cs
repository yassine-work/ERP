using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class CompensationPackage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }      // foreign key
        public Employe Employee { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Base salary must be zero or positive.")]
        public decimal BaseSalary { get; set; }

        [Required]
        public DateTime EffectiveFrom { get; set; } 

        public bool IsActive { get; set; }

        public DateTime? EffectiveTo { get; set; } // nullable → ongoing package

        // Navigation properties, nullable by choice
        public ICollection<EmployeeAdvantage>? Advantages { get; set; }
        public ICollection<EmployeeAllowance>? Allowances { get; set; }
        public ICollection<EmployeeBonus>? Bonuses { get; set; }
    }
}
