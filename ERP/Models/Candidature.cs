using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public enum CandidatureStatus
    {
        Submitted,      // Soumise
        UnderReview,    // En cours d'examen
        Interview,      // Entretien
        Accepted,       // Acceptée
        Rejected,       // Rejetée
        Withdrawn       // Retirée
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
    }
}
