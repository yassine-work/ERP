using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class EmployeAvantage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeId { get; set; }

        [Required]
        public int AvantageId { get; set; }

        [Required]
        public DateTime DateAttribution { get; set; }

        public DateTime? DateExpiration { get; set; }

        [StringLength(500)]
        public string? Conditions { get; set; }

        [Required]
        public string Statut { get; set; } // "Actif", "Suspendu", "Expiré"

        public DateTime? DateSuspension { get; set; }

        public string? RaisonSuspension { get; set; }

        // Navigation properties
        [ForeignKey("EmployeId")]
        public Employe Employe { get; set; }

        [ForeignKey("AvantageId")]
        public Avantage Avantage { get; set; }
    }
}