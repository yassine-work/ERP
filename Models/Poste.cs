using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.Models
{
    public class Poste
    {
        [Key]
        public int PosteId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } // "Software Developer", "HR Manager", etc.

        
        [Required]
        [Range(0, double.MaxValue)]
        public decimal MinimumBaseSalary { get; set; }
        
        // Optional: Add department if positions are grouped
        public string? Department { get; set; }
        
        // Navigation property
        public ICollection<Employe>? Employees { get; set; }
    }
}