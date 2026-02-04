using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public enum CandidatureStatus
    {
        // Initial stages
        Submitted,      // Soumise
        UnderReview,    // En cours d'examen
        Interview,      // Entretien planifié
        
        // Decision stages
        Accepted,       // Acceptée (passé les entretiens)
        Rejected,       // Rejetée
        Withdrawn,      // Retirée par le candidat
        
        // Hiring pipeline (after acceptance)
        OfferSent,      // Offre envoyée
        OfferAccepted,  // Offre acceptée par le candidat
        OfferDeclined,  // Offre refusée par le candidat
        PreBoarding,    // En cours d'intégration (paperasse, etc.)
        Employed        // Embauché → devient employé
    }

    public class Candidature
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Candidat")]
        public int CandidatId { get; set; }

        [ForeignKey("CandidatId")]
        public virtual Candidat? Candidat { get; set; }

        [Required]
        [Display(Name = "Offre d'emploi")]
        public int JobOfferId { get; set; }

        [ForeignKey("JobOfferId")]
        public virtual JobOffer? JobOffer { get; set; }

        [Display(Name = "Lettre de motivation")]
        public string? LettreMotivation { get; set; }

        [Display(Name = "Prétention salariale")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? PretentionSalariale { get; set; }

        [Display(Name = "Disponibilité")]
        [DataType(DataType.Date)]
        public DateTime? DateDisponibilite { get; set; }

        [Display(Name = "Statut")]
        public CandidatureStatus Status { get; set; } = CandidatureStatus.Submitted;

        [Display(Name = "Date de candidature")]
        public DateTime DateCandidature { get; set; } = DateTime.Now;

        [Display(Name = "Notes internes")]
        public string? NotesInternes { get; set; }

        [Display(Name = "Date d'entretien")]
        [DataType(DataType.DateTime)]
        public DateTime? DateEntretien { get; set; }

        [Display(Name = "Retour d'entretien")]
        public string? RetourEntretien { get; set; }

        [Display(Name = "Score/Évaluation")]
        [Range(0, 100)]
        public int? Score { get; set; }
        
        // Hiring pipeline fields
        [Display(Name = "Date d'envoi de l'offre")]
        [DataType(DataType.Date)]
        public DateTime? DateOffreSent { get; set; }
        
        [Display(Name = "Salaire proposé")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalairePropose { get; set; }
        
        [Display(Name = "Date de début prévue")]
        [DataType(DataType.Date)]
        public DateTime? DateDebutPrevue { get; set; }
        
        [Display(Name = "ID Employé créé")]
        public int? EmployeId { get; set; }
        
        [ForeignKey("EmployeId")]
        public virtual Employe? Employe { get; set; }
    }
}
