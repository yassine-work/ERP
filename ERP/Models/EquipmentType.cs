using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class EquipmentType
    {
        [Key]
        public int EquipmentTypeId { get; set; }

        [Required(ErrorMessage = "Le nom du type d'équipement est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Type d'équipement")]
        public string Name { get; set; }

        [MaxLength(500)]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        [Display(Name = "Catégorie")]
        [StringLength(100)]
        public string? Category { get; set; } // "Informatique", "Mobilier", "Vêtements", etc.

        [Display(Name = "Durée de vie estimée (mois)")]
        public int? EstimatedLifespanMonths { get; set; }

        [Display(Name = "Nécessite maintenance")]
        public bool RequiresMaintenance { get; set; } = false;

        // Navigation property
        public ICollection<Equipment>? Equipments { get; set; }
    }
}
