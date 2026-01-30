using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class EmployeeAdvantage
    {
        [Key]
        public int EmployeeAdvantageId { get; set; }

        [Required]
        public int CompensationPackageId { get; set; }
        public CompensationPackage CompensationPackage { get; set; }

        [Required]
        public int AdvantageTypeId { get; set; }
        public AdvantageType AdvantageType { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Value must be zero or positive.")]
        public decimal Value { get; set; } // optional value if needed

        

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime? EndDate { get; set; } // optional, can be null if ongoing
    }
}
