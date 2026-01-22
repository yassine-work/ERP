using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class BenefitType
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } // "Véhicule de fonction"
        
        [StringLength(50)]
        public string Category { get; set; } // "Transport", "Health", "Technology"
        
        public string Description { get; set; }
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal MonetaryValue { get; set; } // Valeur fiscale mensuelle
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal? ActualCost { get; set; } // Coût réel pour l'entreprise
        
        [StringLength(100)]
        public string Provider { get; set; } // Fournisseur
        
        public bool RequiresApproval { get; set; }
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        // Navigation
        public ICollection<EmployeeBenefit> EmployeeBenefits { get; set; }
    }
}
