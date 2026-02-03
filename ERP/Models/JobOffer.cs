using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public enum JobOfferStatus
    {
        Draft,          // Brouillon
        Published,      // Publiée
        Closed,         // Fermée
        Cancelled       // Annulée
    }

    public enum ContractType
    {
        CDI,            // Contrat à Durée Indéterminée
        CDD,            // Contrat à Durée Déterminée
        Stage,          // Stage
        Alternance,     // Alternance
        Freelance       // Freelance
    }

    public class JobOffer
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le titre est obligatoire")]
        [StringLength(200)]
        [Display(Name = "Titre du poste")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "La description est obligatoire")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "Exigences")]
        public string? Requirements { get; set; }

        [Display(Name = "Département")]
        [StringLength(100)]
        public string? Department { get; set; }

        [Display(Name = "Lieu de travail")]
        [StringLength(100)]
        public string? Location { get; set; }

        [Display(Name = "Type de contrat")]
        public ContractType ContractType { get; set; }

        [Display(Name = "Salaire minimum")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalaryMin { get; set; }

        [Display(Name = "Salaire maximum")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalaryMax { get; set; }

        [Display(Name = "Statut")]
        public JobOfferStatus Status { get; set; } = JobOfferStatus.Draft;

        [Display(Name = "Date de publication")]
        [DataType(DataType.Date)]
        public DateTime? PublishedDate { get; set; }

        [Display(Name = "Date limite")]
        [DataType(DataType.Date)]
        public DateTime? ClosingDate { get; set; }

        [Display(Name = "Date de création")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Poste associé")]
        public int? PosteId { get; set; }
        
        [ForeignKey("PosteId")]
        public virtual Poste? Poste { get; set; }

        // Navigation property
        public virtual ICollection<Candidature>? Candidatures { get; set; }
    }
}
