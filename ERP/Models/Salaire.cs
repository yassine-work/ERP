using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Salaire
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaireBase { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PrimePerformance { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PrimeAnciennete { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AutresPrimes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? HeuresSupplementaires { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TauxHeureSup { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaireBrut { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? CotisationsSociales { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ImpotRevenu { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? AutresRetenues { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalaireNet { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }

        public DateTime? DateFin { get; set; }

        [Required]
        public string PeriodePaiement { get; set; } // "Mensuel", "Bimensuel", "Hebdomadaire"
        
        [Required]
        public DateTime DateCreation { get; set; }

        public DateTime? DateModification { get; set; }

        public string? Commentaires { get; set; }

        // Navigation property
        [ForeignKey("EmployeId")]
        public Employe Employe { get; set; }
    }
}