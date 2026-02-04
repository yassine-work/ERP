using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public enum EmployeeStatus
    {
        Active,         // En activité
        OnLeave,        // En congé
        Suspended,      // Suspendu
        Terminated,     // Licencié
        Resigned        // Démissionnaire
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }

    public class Employe
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le matricule est obligatoire")]
        [StringLength(20)]
        [Display(Name = "Matricule")]
        public string Matricule { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [Display(Name = "Nom")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [Display(Name = "Sexe")]
        public Gender? Gender { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance")]
        public DateTime DateNaissance { get; set; }

        [Display(Name = "Lieu de naissance")]
        [StringLength(100)]
        public string? LieuNaissance { get; set; }

        [Display(Name = "CIN / Pièce d'identité")]
        [StringLength(50)]
        public string? CIN { get; set; }

        [Display(Name = "Nationalité")]
        [StringLength(50)]
        public string? Nationalite { get; set; }

        [Display(Name = "Situation familiale")]
        [StringLength(50)]
        public string? SituationFamiliale { get; set; } // Célibataire, Marié(e), etc.

        [Display(Name = "Nombre d'enfants")]
        [Range(0, 20)]
        public int? NombreEnfants { get; set; }

        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Format de téléphone invalide")]
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }

        [Display(Name = "Téléphone d'urgence")]
        [Phone(ErrorMessage = "Format de téléphone invalide")]
        public string? TelephoneUrgence { get; set; }

        [Display(Name = "Contact d'urgence")]
        [StringLength(100)]
        public string? ContactUrgence { get; set; }

        [Display(Name = "Adresse")]
        public string Adresse { get; set; }

        [Display(Name = "Ville")]
        [StringLength(100)]
        public string? Ville { get; set; }

        [Display(Name = "Code postal")]
        [StringLength(10)]
        public string? CodePostal { get; set; }
        
        [Required]
        [Display(Name = "Poste")]
        public int PosteId { get; set; } 
        
        [ForeignKey("PosteId")]
        public Poste? Poste { get; set; } 

        [DataType(DataType.Date)]
        [Display(Name = "Date d'embauche")]
        public DateTime DateEmbauche { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date de fin de contrat")]
        public DateTime? DateFinContrat { get; set; }

        [Display(Name = "Type de contrat")]
        [StringLength(50)]
        public string? TypeContrat { get; set; } // CDI, CDD, Stage, etc.

        [Display(Name = "Statut")]
        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

        [Display(Name = "Photo")]
        public string? PhotoUrl { get; set; }

        [Display(Name = "Notes")]
        [StringLength(1000)]
        public string? Notes { get; set; }

        // Navigation properties
        public ICollection<CompensationPackage>? CompensationPackages { get; set; }
        public ICollection<EquipmentAssignment>? EquipmentAssignments { get; set; }
    }
}