using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public enum EquipmentStatus
    {
        Available,      // Disponible
        Assigned,       // Attribué
        InMaintenance,  // En maintenance
        Retired,        // Mis au rebut
        Lost            // Perdu
    }

    public class Equipment
    {
        [Key]
        public int EquipmentId { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(200)]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Display(Name = "Numéro de série")]
        [StringLength(100)]
        public string? SerialNumber { get; set; }

        [Display(Name = "Code d'inventaire")]
        [StringLength(50)]
        public string? InventoryCode { get; set; }

        [Required]
        [Display(Name = "Type d'équipement")]
        public int EquipmentTypeId { get; set; }

        [ForeignKey("EquipmentTypeId")]
        public EquipmentType? EquipmentType { get; set; }

        [Display(Name = "Marque")]
        [StringLength(100)]
        public string? Brand { get; set; }

        [Display(Name = "Modèle")]
        [StringLength(100)]
        public string? Model { get; set; }

        [Display(Name = "Date d'acquisition")]
        [DataType(DataType.Date)]
        public DateTime? AcquisitionDate { get; set; }

        [Display(Name = "Prix d'achat")]
        [Range(0, double.MaxValue)]
        [DataType(DataType.Currency)]
        public decimal? PurchasePrice { get; set; }

        [Display(Name = "Fournisseur")]
        [StringLength(200)]
        public string? Supplier { get; set; }

        [Display(Name = "Date de fin de garantie")]
        [DataType(DataType.Date)]
        public DateTime? WarrantyEndDate { get; set; }

        [Display(Name = "Statut")]
        public EquipmentStatus Status { get; set; } = EquipmentStatus.Available;

        [Display(Name = "Notes")]
        [MaxLength(1000)]
        public string? Notes { get; set; }

        // Navigation property for assignments
        public ICollection<EquipmentAssignment>? Assignments { get; set; }

        // Helper property to get current assignment
        [NotMapped]
        public EquipmentAssignment? CurrentAssignment => 
            Assignments?.FirstOrDefault(a => a.ReturnDate == null);
    }
}
