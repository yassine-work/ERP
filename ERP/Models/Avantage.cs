using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Avantage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TypeAvantage { get; set; } // "Mutuelle", "Ticket Resto", "Transport", "Retraite", "Autre"

        [Required]
        [StringLength(200)]
        public string Libelle { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MontantEmploye { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MontantEmployeur { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? MontantTotal { get; set; }

        [Required]
        public DateTime DateDebut { get; set; }

        public DateTime? DateFin { get; set; }

        [Required]
        public string Statut { get; set; } // "Actif", "Inactif", "En attente"

        public string? Description { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        public DateTime? DateModification { get; set; }
    }
}