using System;
using System.ComponentModel.DataAnnotations;
namespace ERP.Models
{  

    public class BonusType
    {
        [Key]
        public int BonusTypeId { get; set; }

        [Required(ErrorMessage = "Le nom du bonus est obligatoire")]
        public string BonusTypeName { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; } // optional description

        [MaxLength(500)]
        public string? BonusRule { get; set; } // conditions or formula for calculating bonus

        public bool IsTaxable { get; set; } = true; // whether this bonus is taxable

        public bool IsAutomatic { get; set; } = false; // whether automatically applied
    }
}
