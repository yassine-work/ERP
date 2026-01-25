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
    }
}
