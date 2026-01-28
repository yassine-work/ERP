using System;
using System.ComponentModel.DataAnnotations;
namespace ERP.Models
{  

    public class AllowanceType
    {
        [Key]
        public int AllowanceTypeId { get; set; }

        [Required(ErrorMessage = "Le nom de l'allocation est obligatoire")]
        public string AllowanceTypeName { get; set; } // "Transport", "Housing"

        [MaxLength(500)]
        public string? Description { get; set; } // optional description

        [MaxLength(100)]
        public string? Provider { get; set; } // optional, e.g., transport company

        [MaxLength(500)]
        public string? EligibilityRule { get; set; } // optional, conditions or notes

        public bool IsTaxable { get; set; } = true; // whether this allowance is taxable
    }

}
