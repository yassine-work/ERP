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
    }

}
