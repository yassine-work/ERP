using ERP.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

    public static void SeedEquipmentTypes(ModelBuilder modelBuilder)
    {
        var equipmentTypes = new List<EquipmentType>
        {
            new EquipmentType
            {
                EquipmentTypeId = 1,
                Name = "Ordinateur Portable",
                Description = "Ordinateur portable professionnel pour le travail",
                Category = "Informatique",
                EstimatedLifespanMonths = 48,
                RequiresMaintenance = true
            },
            new EquipmentType
            {
                EquipmentTypeId = 2,
                Name = "Ordinateur de Bureau",
                Description = "Ordinateur fixe avec écran pour le bureau",
                Category = "Informatique",
                EstimatedLifespanMonths = 60,
                RequiresMaintenance = true
            },
            new EquipmentType
            {
                EquipmentTypeId = 3,
                Name = "Téléphone Mobile",
                Description = "Smartphone professionnel",
                Category = "Informatique",
                EstimatedLifespanMonths = 24,
                RequiresMaintenance = false
            },
            new EquipmentType
            {
                EquipmentTypeId = 4,
                Name = "Badge d'accès",
                Description = "Badge d'accès aux locaux de l'entreprise",
                Category = "Sécurité",
                EstimatedLifespanMonths = null,
                RequiresMaintenance = false
            },
            new EquipmentType
            {
                EquipmentTypeId = 5,
                Name = "Clés de bureau",
                Description = "Trousseau de clés pour accès aux bureaux",
                Category = "Sécurité",
                EstimatedLifespanMonths = null,
                RequiresMaintenance = false
            },
            new EquipmentType
            {
                EquipmentTypeId = 6,
                Name = "Tenue de travail",
                Description = "Uniforme ou vêtements de travail fournis",
                Category = "Vêtements",
                EstimatedLifespanMonths = 12,
                RequiresMaintenance = false
            },
            new EquipmentType
            {
                EquipmentTypeId = 7,
                Name = "Équipement de sécurité",
                Description = "Casque, gants, lunettes de protection, etc.",
                Category = "Sécurité",
                EstimatedLifespanMonths = 12,
                RequiresMaintenance = false
            },
            new EquipmentType
            {
                EquipmentTypeId = 8,
                Name = "Mobilier de bureau",
                Description = "Bureau, chaise ergonomique, etc.",
                Category = "Mobilier",
                EstimatedLifespanMonths = 120,
                RequiresMaintenance = false
            },
            new EquipmentType
            {
                EquipmentTypeId = 9,
                Name = "Véhicule de service",
                Description = "Voiture ou véhicule de fonction",
                Category = "Transport",
                EstimatedLifespanMonths = 60,
                RequiresMaintenance = true
            },
            new EquipmentType
            {
                EquipmentTypeId = 10,
                Name = "Écran supplémentaire",
                Description = "Moniteur additionnel pour le poste de travail",
                Category = "Informatique",
                EstimatedLifespanMonths = 72,
                RequiresMaintenance = false
            }
        };

        modelBuilder.Entity<EquipmentType>().HasData(equipmentTypes);
    }

    public static void SeedEquipments(ModelBuilder modelBuilder)
    {
        var equipments = new List<Equipment>
        {
            // Laptops
            new Equipment
            {
                EquipmentId = 1,
                Name = "MacBook Pro 14\"",
                SerialNumber = "C02X12345678",
                InventoryCode = "LAP-001",
                EquipmentTypeId = 1,
                Brand = "Apple",
                Model = "MacBook Pro 14\" M3",
                AcquisitionDate = new DateTime(2025, 1, 15),
                PurchasePrice = 2499.00m,
                Supplier = "Apple Store",
                WarrantyEndDate = new DateTime(2028, 1, 15),
                Status = EquipmentStatus.Available
            },
            new Equipment
            {
                EquipmentId = 2,
                Name = "ThinkPad X1 Carbon",
                SerialNumber = "PF2ABCD1234",
                InventoryCode = "LAP-002",
                EquipmentTypeId = 1,
                Brand = "Lenovo",
                Model = "ThinkPad X1 Carbon Gen 11",
                AcquisitionDate = new DateTime(2025, 2, 1),
                PurchasePrice = 1899.00m,
                Supplier = "Lenovo Direct",
                WarrantyEndDate = new DateTime(2028, 2, 1),
                Status = EquipmentStatus.Available
            },
            new Equipment
            {
                EquipmentId = 3,
                Name = "Dell Latitude 5540",
                SerialNumber = "DELL12345ABC",
                InventoryCode = "LAP-003",
                EquipmentTypeId = 1,
                Brand = "Dell",
                Model = "Latitude 5540",
                AcquisitionDate = new DateTime(2025, 3, 10),
                PurchasePrice = 1299.00m,
                Supplier = "Dell Business",
                WarrantyEndDate = new DateTime(2028, 3, 10),
                Status = EquipmentStatus.Available
            },
            // Desktop
            new Equipment
            {
                EquipmentId = 4,
                Name = "Dell OptiPlex 7010",
                SerialNumber = "OPTIPLEX7890",
                InventoryCode = "DES-001",
                EquipmentTypeId = 2,
                Brand = "Dell",
                Model = "OptiPlex 7010",
                AcquisitionDate = new DateTime(2024, 6, 20),
                PurchasePrice = 899.00m,
                Supplier = "Dell Business",
                WarrantyEndDate = new DateTime(2027, 6, 20),
                Status = EquipmentStatus.Available
            },
            // Phones
            new Equipment
            {
                EquipmentId = 5,
                Name = "iPhone 15 Pro",
                SerialNumber = "DQWERTY12345",
                InventoryCode = "PHN-001",
                EquipmentTypeId = 3,
                Brand = "Apple",
                Model = "iPhone 15 Pro 256GB",
                AcquisitionDate = new DateTime(2025, 1, 5),
                PurchasePrice = 1199.00m,
                Supplier = "Apple Store",
                WarrantyEndDate = new DateTime(2027, 1, 5),
                Status = EquipmentStatus.Available
            },
            new Equipment
            {
                EquipmentId = 6,
                Name = "Samsung Galaxy S24",
                SerialNumber = "RFCM12345678",
                InventoryCode = "PHN-002",
                EquipmentTypeId = 3,
                Brand = "Samsung",
                Model = "Galaxy S24 Ultra",
                AcquisitionDate = new DateTime(2025, 2, 15),
                PurchasePrice = 1099.00m,
                Supplier = "Samsung Business",
                WarrantyEndDate = new DateTime(2027, 2, 15),
                Status = EquipmentStatus.Available
            },
            // Access badges
            new Equipment
            {
                EquipmentId = 7,
                Name = "Badge Accès Principal",
                SerialNumber = "BADGE-001",
                InventoryCode = "BAD-001",
                EquipmentTypeId = 4,
                Brand = "HID",
                Model = "iCLASS SE",
                AcquisitionDate = new DateTime(2024, 1, 1),
                PurchasePrice = 15.00m,
                Status = EquipmentStatus.Available
            },
            new Equipment
            {
                EquipmentId = 8,
                Name = "Badge Accès Principal",
                SerialNumber = "BADGE-002",
                InventoryCode = "BAD-002",
                EquipmentTypeId = 4,
                Brand = "HID",
                Model = "iCLASS SE",
                AcquisitionDate = new DateTime(2024, 1, 1),
                PurchasePrice = 15.00m,
                Status = EquipmentStatus.Available
            },
            // Monitors
            new Equipment
            {
                EquipmentId = 9,
                Name = "Dell UltraSharp 27\"",
                SerialNumber = "MONITOR-001",
                InventoryCode = "MON-001",
                EquipmentTypeId = 10,
                Brand = "Dell",
                Model = "UltraSharp U2722D",
                AcquisitionDate = new DateTime(2024, 8, 15),
                PurchasePrice = 549.00m,
                Supplier = "Dell Business",
                WarrantyEndDate = new DateTime(2027, 8, 15),
                Status = EquipmentStatus.Available
            },
            new Equipment
            {
                EquipmentId = 10,
                Name = "LG 34\" Curved",
                SerialNumber = "MONITOR-002",
                InventoryCode = "MON-002",
                EquipmentTypeId = 10,
                Brand = "LG",
                Model = "34WN80C-B",
                AcquisitionDate = new DateTime(2024, 9, 1),
                PurchasePrice = 699.00m,
                Supplier = "Amazon Business",
                WarrantyEndDate = new DateTime(2027, 9, 1),
                Status = EquipmentStatus.Available
            }
        };

        modelBuilder.Entity<Equipment>().HasData(equipments);
    }

    public static void SeedAll(ModelBuilder modelBuilder)
    {
        SeedPostes(modelBuilder);
        SeedAllowanceTypes(modelBuilder);
        SeedBonusTypes(modelBuilder);
        SeedAdvantageTypes(modelBuilder);
        SeedEquipmentTypes(modelBuilder);
        SeedEquipments(modelBuilder);
    }
}
