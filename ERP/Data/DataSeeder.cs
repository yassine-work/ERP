using ERP.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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

    public static void SeedAllowanceTypes(ModelBuilder modelBuilder)
    {
        var allowanceTypes = new List<AllowanceType>
        {
            new AllowanceType
            {
                AllowanceTypeId = 1,
                AllowanceTypeName = "Transport",
                Description = "Allocation pour les frais de transport domicile-travail",
                Provider = "RATP / Transports publics",
                EligibilityRule = "Tous les employés",
                IsTaxable = false
            },
            new AllowanceType
            {
                AllowanceTypeId = 2,
                AllowanceTypeName = "Meal",
                Description = "Allocation pour les repas et restauration",
                Provider = "Restaurant d'entreprise",
                EligibilityRule = "Tous les employés en CDI",
                IsTaxable = false
            },
            new AllowanceType
            {
                AllowanceTypeId = 3,
                AllowanceTypeName = "Housing",
                Description = "Allocation pour les frais de logement",
                Provider = "Auto-géré",
                EligibilityRule = "Sur demande et justification de domicile",
                IsTaxable = true
            },
            new AllowanceType
            {
                AllowanceTypeId = 4,
                AllowanceTypeName = "Phone",
                Description = "Allocation pour usage professionnel du téléphone",
                Provider = "Forfait opérateur",
                EligibilityRule = "Employés en contact client ou télétravail",
                IsTaxable = false
            }
        };

        modelBuilder.Entity<AllowanceType>().HasData(allowanceTypes);
    }

    public static void SeedBonusTypes(ModelBuilder modelBuilder)
    {
        var bonusTypes = new List<BonusType>
        {
            new BonusType
            {
                BonusTypeId = 1,
                BonusTypeName = "Performance",
                Description = "Prime basée sur la performance et les objectifs atteints",
                BonusRule = "Basée sur les KPIs individuels, entre 0-20% du salaire mensuel",
                IsTaxable = true,
                IsAutomatic = false
            },
            new BonusType
            {
                BonusTypeId = 2,
                BonusTypeName = "Referral",
                Description = "Prime pour recommandation et recrutement de candidats",
                BonusRule = "500-2000€ selon le poste recruté et la durée de permanence",
                IsTaxable = true,
                IsAutomatic = false
            },
            new BonusType
            {
                BonusTypeId = 3,
                BonusTypeName = "Year-end",
                Description = "Prime de fin d'année versée en décembre",
                BonusRule = "1 mois de salaire minimum, variable selon ancienneté",
                IsTaxable = true,
                IsAutomatic = true
            }
        };

        modelBuilder.Entity<BonusType>().HasData(bonusTypes);
    }

    public static void SeedAdvantageTypes(ModelBuilder modelBuilder)
    {
        var advantageTypes = new List<AdvantageType>
        {
            new AdvantageType
            {
                AdvantageTypeId = 1,
                AdvantageTypeName = "Health Insurance",
                Description = "Assurance santé complémentaire pour l'employé et sa famille",
                Provider = "Global Health",
                EligibilityRule = "Full-time employees only",
                IsTaxable = false
            },
            new AdvantageType
            {
                AdvantageTypeId = 2,
                AdvantageTypeName = "Gym Membership",
                Description = "Accès à une salle de sport pour la santé et le bien-être",
                Provider = "FitClub",
                EligibilityRule = "Available after 3 months of employment",
                IsTaxable = false
            },
            new AdvantageType
            {
                AdvantageTypeId = 3,
                AdvantageTypeName = "Company Car",
                Description = "Mise à disposition d'un véhicule de fonction",
                Provider = "Leasing Co.",
                EligibilityRule = "Managers only",
                IsTaxable = true
            }
        };

        modelBuilder.Entity<AdvantageType>().HasData(advantageTypes);
    }

    public static void SeedAll(ModelBuilder modelBuilder)
    {
        SeedPostes(modelBuilder);
        SeedAllowanceTypes(modelBuilder);
        SeedBonusTypes(modelBuilder);
        SeedAdvantageTypes(modelBuilder);
    }
}
