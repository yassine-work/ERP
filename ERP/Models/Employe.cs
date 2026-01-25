using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Models
{
    public class Employe
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est obligatoire")]
        public string Nom { get; set; }

        [Required(ErrorMessage = "Le prénom est obligatoire")]
        public string Prenom { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Telephone { get; set; }
        public string Adresse { get; set; }
        
        [Required]
        public int PosteId { get; set; } 
        
        [ForeignKey("PosteId")]
        public Poste? Poste { get; set; } 

        [DataType(DataType.Date)]
        public DateTime DateEmbauche { get; set; }

        public ICollection<CompensationPackage>? CompensationPackages { get; set; }

        
    }
}