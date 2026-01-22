using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class EmployeeAllowance
    {
        public int Id { get; set; }
        
        public int EmployeeId { get; set; }
        public int AllowanceTypeId { get; set; }
        
        [Column(TypeName = "decimal(18,3)")]
        public decimal Amount { get; set; }
        
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        
        [StringLength(20)]
        public string Status { get; set; } // "Active", "Suspended", "Ended"
        
        public string Notes { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        
        // Navigation
        public Employe employe { get; set; }
        public AllowanceType AllowanceType { get; set; }
    }
}