using ERP.Models;
using Microsoft.EntityFrameworkCore;


public static class DataSeeder
{
    public static void SeedPostes(ModelBuilder modelBuilder)
    {
        var postes = new List<Poste>
        {
                        // IT
            new Poste { PosteId = 1, Title = "Junior Developer", Department = "IT", MinimumBaseSalary = 2500 },
            new Poste { PosteId = 2, Title = "Senior Developer", Department = "IT", MinimumBaseSalary = 5000 },
            new Poste { PosteId = 3, Title = "Full Stack Developer", Department = "IT", MinimumBaseSalary = 4000 },
            new Poste { PosteId = 4, Title = "DevOps Engineer", Department = "IT", MinimumBaseSalary = 4500 },
            new Poste { PosteId = 5, Title = "IT Manager", Department = "IT", MinimumBaseSalary = 7000 },
            new Poste { PosteId = 6, Title = "System Administrator", Department = "IT", MinimumBaseSalary = 3500 },
            new Poste { PosteId = 7, Title = "QA Engineer", Department = "IT", MinimumBaseSalary = 3000 },

            // Human Resources
            new Poste { PosteId = 8, Title = "HR Specialist", Department = "Human Resources", MinimumBaseSalary = 3000 },
            new Poste { PosteId = 9, Title = "Recruiter", Department = "Human Resources", MinimumBaseSalary = 2800 },
            new Poste { PosteId = 10, Title = "HR Manager", Department = "Human Resources", MinimumBaseSalary = 5500 },
            new Poste { PosteId = 11, Title = "Training Coordinator", Department = "Human Resources", MinimumBaseSalary = 3200 },
            new Poste { PosteId = 12, Title = "Payroll Specialist", Department = "Human Resources", MinimumBaseSalary = 3000 },

            // Finance
            new Poste { PosteId = 13, Title = "Accountant", Department = "Finance", MinimumBaseSalary = 3200 },
            new Poste { PosteId = 14, Title = "Financial Analyst", Department = "Finance", MinimumBaseSalary = 4000 },
            new Poste { PosteId = 15, Title = "Senior Accountant", Department = "Finance", MinimumBaseSalary = 4500 },
            new Poste { PosteId = 16, Title = "Finance Manager", Department = "Finance", MinimumBaseSalary = 6000 },

            // Sales
            new Poste { PosteId = 17, Title = "Sales Representative", Department = "Sales", MinimumBaseSalary = 2500 },
            new Poste { PosteId = 18, Title = "Sales Manager", Department = "Sales", MinimumBaseSalary = 5000 },

            // Marketing
            new Poste { PosteId = 19, Title = "Marketing Specialist", Department = "Marketing", MinimumBaseSalary = 3200 },
            new Poste { PosteId = 20, Title = "Digital Marketing Manager", Department = "Marketing", MinimumBaseSalary = 4500 },
            new Poste { PosteId = 21, Title = "Content Creator", Department = "Marketing", MinimumBaseSalary = 2800 },

            // Operations
            new Poste { PosteId = 22, Title = "Operations Manager", Department = "Operations", MinimumBaseSalary = 5500 },
            new Poste { PosteId = 23, Title = "Project Manager", Department = "Operations", MinimumBaseSalary = 4500 },
            new Poste { PosteId = 24, Title = "Administrative Assistant", Department = "Operations", MinimumBaseSalary = 2200 },
            new Poste { PosteId = 25, Title = "Business Analyst", Department = "Operations", MinimumBaseSalary = 3800 },

            // Customer Support
            new Poste { PosteId = 26, Title = "Customer Support Agent", Department = "Customer Support", MinimumBaseSalary = 2400 },
            new Poste { PosteId = 27, Title = "Customer Support Manager", Department = "Customer Support", MinimumBaseSalary = 4200 },
            new Poste { PosteId = 28, Title = "Technical Support Specialist", Department = "Customer Support", MinimumBaseSalary = 3000 }
            
            
        };
        
        modelBuilder.Entity<Poste>().HasData(postes);
    }
}