using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class BulletinPaie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SalaireId { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroBulletin { get; set; }

        [Required]
        public int Mois { get; set; }

        [Required]
        public int Annee { get; set; }

        [Required]
        public DateTime DatePaiement { get; set; }

        [Required]
        [StringLength(3)]
        public string Devise { get; set; } = "EUR";

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAvantages { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalRetenues { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal NetAPayer { get; set; }

        public string? DetailsSupplementaires { get; set; }

        [StringLength(50)]
        public string? ModePaiement { get; set; } // "Virement", "Chèque", "Espèces"

        [StringLength(100)]
        public string? ReferenceTransaction { get; set; }

        [Required]
        public bool EstPaye { get; set; } = false;

        public DateTime? DatePaiementEffectif { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        // Navigation property
        [ForeignKey("SalaireId")]
        public Salaire Salaire { get; set; }
    }
}