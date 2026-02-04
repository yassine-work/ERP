using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class Candidat
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Nom")]
        public string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        [StringLength(100)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "L'email est obligatoire")]
        [EmailAddress(ErrorMessage = "Format d'email invalide")]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [Display(Name = "Téléphone")]
        public string? Telephone { get; set; }

        [Display(Name = "Adresse")]
        public string? Adresse { get; set; }

        [Display(Name = "Ville")]
        [StringLength(100)]
        public string? Ville { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date de naissance")]
        public DateTime? DateNaissance { get; set; }

        [Display(Name = "Niveau d'études")]
        [StringLength(100)]
        public string? NiveauEtudes { get; set; }

        [Display(Name = "Expérience (années)")]
        public int? AnneeExperience { get; set; }

        [Display(Name = "Compétences")]
        public string? Competences { get; set; }

        [Display(Name = "Lien LinkedIn")]
        [Url]
        public string? LinkedInUrl { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Display(Name = "Date d'inscription")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public virtual ICollection<Candidature>? Candidatures { get; set; }

        [Display(Name = "Nom complet")]
        public string NomComplet => $"{Prenom} {Nom}";
    }
}
