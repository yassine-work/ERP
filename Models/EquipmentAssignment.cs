using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public enum AssignmentCondition
    {
        New,        // Neuf
        Good,       // Bon état
        Fair,       // État correct
        Poor        // Mauvais état
    }

    public class EquipmentAssignment
    {
        [Key]
        public int AssignmentId { get; set; }

        [Required]
        [Display(Name = "Équipement")]
        public int EquipmentId { get; set; }

        [ForeignKey("EquipmentId")]
        public Equipment? Equipment { get; set; }

        [Required]
        [Display(Name = "Employé")]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employe? Employee { get; set; }

        [Required]
        [Display(Name = "Date d'attribution")]
        [DataType(DataType.Date)]
        public DateTime AssignmentDate { get; set; }

        [Display(Name = "Date de retour")]
        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; }

        [Display(Name = "État à l'attribution")]
        public AssignmentCondition ConditionAtAssignment { get; set; } = AssignmentCondition.Good;

        [Display(Name = "État au retour")]
        public AssignmentCondition? ConditionAtReturn { get; set; }

        [Display(Name = "Attribué par")]
        [StringLength(100)]
        public string? AssignedBy { get; set; }

        [Display(Name = "Notes d'attribution")]
        [MaxLength(500)]
        public string? AssignmentNotes { get; set; }

        [Display(Name = "Notes de retour")]
        [MaxLength(500)]
        public string? ReturnNotes { get; set; }

        // Helper property to check if assignment is active
        [NotMapped]
        public bool IsActive => ReturnDate == null;
    }
}
