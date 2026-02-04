using ERP.Models;

namespace ERP.Data
{
    /// <summary>
    /// Seeds data at runtime for sandbox sessions.
    /// This is separate from the ModelBuilder seeder because in-memory databases
    /// don't run migrations - they need data inserted directly.
    /// </summary>
    public static class RuntimeDataSeeder
    {
        public static void SeedAllData(AppDbContext context)
        {
            // Seed in order of dependencies
            SeedPostes(context);
            SeedAllowanceTypes(context);
            SeedBonusTypes(context);
            SeedAdvantageTypes(context);
            SeedEquipmentTypes(context);
            SeedEquipments(context);
            SeedEmployees(context);
            SeedJobOffers(context);
            SeedCandidatesAndCandidatures(context);
            
            context.SaveChanges();
        }

        private static void SeedPostes(AppDbContext context)
        {
            if (context.Postes.Any()) return;

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

            context.Postes.AddRange(postes);
            context.SaveChanges();
        }

        private static void SeedAllowanceTypes(AppDbContext context)
        {
            if (context.AllowanceTypes.Any()) return;

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

            context.AllowanceTypes.AddRange(allowanceTypes);
            context.SaveChanges();
        }

        private static void SeedBonusTypes(AppDbContext context)
        {
            if (context.BonusTypes.Any()) return;

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

            context.BonusTypes.AddRange(bonusTypes);
            context.SaveChanges();
        }

        private static void SeedAdvantageTypes(AppDbContext context)
        {
            if (context.AdvantageTypes.Any()) return;

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

            context.AdvantageTypes.AddRange(advantageTypes);
            context.SaveChanges();
        }

        private static void SeedEquipmentTypes(AppDbContext context)
        {
            if (context.EquipmentTypes.Any()) return;

            var equipmentTypes = new List<EquipmentType>
            {
                new EquipmentType { EquipmentTypeId = 1, Name = "Ordinateur Portable", Description = "Ordinateur portable professionnel pour le travail", Category = "Informatique", EstimatedLifespanMonths = 48, RequiresMaintenance = true },
                new EquipmentType { EquipmentTypeId = 2, Name = "Ordinateur de Bureau", Description = "Ordinateur fixe avec écran pour le bureau", Category = "Informatique", EstimatedLifespanMonths = 60, RequiresMaintenance = true },
                new EquipmentType { EquipmentTypeId = 3, Name = "Téléphone Mobile", Description = "Smartphone professionnel", Category = "Informatique", EstimatedLifespanMonths = 24, RequiresMaintenance = false },
                new EquipmentType { EquipmentTypeId = 4, Name = "Badge d'accès", Description = "Badge d'accès aux locaux de l'entreprise", Category = "Sécurité", EstimatedLifespanMonths = null, RequiresMaintenance = false },
                new EquipmentType { EquipmentTypeId = 5, Name = "Clés de bureau", Description = "Trousseau de clés pour accès aux bureaux", Category = "Sécurité", EstimatedLifespanMonths = null, RequiresMaintenance = false },
                new EquipmentType { EquipmentTypeId = 6, Name = "Tenue de travail", Description = "Uniforme ou vêtements de travail fournis", Category = "Vêtements", EstimatedLifespanMonths = 12, RequiresMaintenance = false },
                new EquipmentType { EquipmentTypeId = 7, Name = "Équipement de sécurité", Description = "Casque, gants, lunettes de protection, etc.", Category = "Sécurité", EstimatedLifespanMonths = 12, RequiresMaintenance = false },
                new EquipmentType { EquipmentTypeId = 8, Name = "Mobilier de bureau", Description = "Bureau, chaise ergonomique, etc.", Category = "Mobilier", EstimatedLifespanMonths = 120, RequiresMaintenance = false },
                new EquipmentType { EquipmentTypeId = 9, Name = "Véhicule de service", Description = "Voiture ou véhicule de fonction", Category = "Transport", EstimatedLifespanMonths = 60, RequiresMaintenance = true },
                new EquipmentType { EquipmentTypeId = 10, Name = "Écran supplémentaire", Description = "Moniteur additionnel pour le poste de travail", Category = "Informatique", EstimatedLifespanMonths = 72, RequiresMaintenance = false }
            };

            context.EquipmentTypes.AddRange(equipmentTypes);
            context.SaveChanges();
        }

        private static void SeedEquipments(AppDbContext context)
        {
            if (context.Equipments.Any()) return;

            var equipments = new List<Equipment>
            {
                new Equipment { EquipmentId = 1, Name = "MacBook Pro 14\"", SerialNumber = "C02X12345678", InventoryCode = "LAP-001", EquipmentTypeId = 1, Brand = "Apple", Model = "MacBook Pro 14\" M3", AcquisitionDate = new DateTime(2025, 1, 15), PurchasePrice = 2499.00m, Supplier = "Apple Store", WarrantyEndDate = new DateTime(2028, 1, 15), Status = EquipmentStatus.Available },
                new Equipment { EquipmentId = 2, Name = "ThinkPad X1 Carbon", SerialNumber = "PF2ABCD1234", InventoryCode = "LAP-002", EquipmentTypeId = 1, Brand = "Lenovo", Model = "ThinkPad X1 Carbon Gen 11", AcquisitionDate = new DateTime(2025, 2, 1), PurchasePrice = 1899.00m, Supplier = "Lenovo Direct", WarrantyEndDate = new DateTime(2028, 2, 1), Status = EquipmentStatus.Available },
                new Equipment { EquipmentId = 3, Name = "Dell Latitude 5540", SerialNumber = "DELL12345ABC", InventoryCode = "LAP-003", EquipmentTypeId = 1, Brand = "Dell", Model = "Latitude 5540", AcquisitionDate = new DateTime(2025, 3, 10), PurchasePrice = 1299.00m, Supplier = "Dell Business", WarrantyEndDate = new DateTime(2028, 3, 10), Status = EquipmentStatus.Available },
                new Equipment { EquipmentId = 4, Name = "Dell OptiPlex 7010", SerialNumber = "OPTIPLEX7890", InventoryCode = "DES-001", EquipmentTypeId = 2, Brand = "Dell", Model = "OptiPlex 7010", AcquisitionDate = new DateTime(2024, 6, 20), PurchasePrice = 899.00m, Supplier = "Dell Business", WarrantyEndDate = new DateTime(2027, 6, 20), Status = EquipmentStatus.Available },
                new Equipment { EquipmentId = 5, Name = "iPhone 15 Pro", SerialNumber = "DQWERTY12345", InventoryCode = "PHN-001", EquipmentTypeId = 3, Brand = "Apple", Model = "iPhone 15 Pro 256GB", AcquisitionDate = new DateTime(2025, 1, 5), PurchasePrice = 1199.00m, Supplier = "Apple Store", WarrantyEndDate = new DateTime(2027, 1, 5), Status = EquipmentStatus.Available },
                new Equipment { EquipmentId = 6, Name = "Samsung Galaxy S24", SerialNumber = "RFCM12345678", InventoryCode = "PHN-002", EquipmentTypeId = 3, Brand = "Samsung", Model = "Galaxy S24 Ultra", AcquisitionDate = new DateTime(2025, 2, 15), PurchasePrice = 1099.00m, Supplier = "Samsung Business", WarrantyEndDate = new DateTime(2027, 2, 15), Status = EquipmentStatus.Available },
                new Equipment { EquipmentId = 7, Name = "Badge Accès Principal", SerialNumber = "BADGE-001", InventoryCode = "BAD-001", EquipmentTypeId = 4, Brand = "HID", Model = "iCLASS SE", AcquisitionDate = new DateTime(2024, 1, 1), PurchasePrice = 15.00m, Status = EquipmentStatus.Available },
                new Equipment { EquipmentId = 8, Name = "Badge Accès Principal", SerialNumber = "BADGE-002", InventoryCode = "BAD-002", EquipmentTypeId = 4, Brand = "HID", Model = "iCLASS SE", AcquisitionDate = new DateTime(2024, 1, 1), PurchasePrice = 15.00m, Status = EquipmentStatus.Available },
                new Equipment { EquipmentId = 9, Name = "Dell UltraSharp 27\"", SerialNumber = "MONITOR-001", InventoryCode = "MON-001", EquipmentTypeId = 10, Brand = "Dell", Model = "UltraSharp U2722D", AcquisitionDate = new DateTime(2024, 8, 15), PurchasePrice = 549.00m, Supplier = "Dell Business", WarrantyEndDate = new DateTime(2027, 8, 15), Status = EquipmentStatus.Available },
                new Equipment { EquipmentId = 10, Name = "LG 34\" Curved", SerialNumber = "MONITOR-002", InventoryCode = "MON-002", EquipmentTypeId = 10, Brand = "LG", Model = "34WN80C-B", AcquisitionDate = new DateTime(2024, 9, 1), PurchasePrice = 699.00m, Supplier = "Amazon Business", WarrantyEndDate = new DateTime(2027, 9, 1), Status = EquipmentStatus.Available }
            };

            context.Equipments.AddRange(equipments);
            context.SaveChanges();
        }

        private static void SeedEmployees(AppDbContext context)
        {
            if (context.Employes.Any()) return;

            var employees = new List<Employe>
            {
                new Employe { Id = 1, Matricule = "EMP001", Nom = "Martin", Prenom = "Alice", Email = "alice.martin@erp.demo", Telephone = "+33 6 12 34 56 78", Adresse = "12 Rue de Paris, 75001 Paris", DateNaissance = new DateTime(1990, 5, 15), DateEmbauche = new DateTime(2022, 3, 1), PosteId = 2, Status = EmployeeStatus.Active },
                new Employe { Id = 2, Matricule = "EMP002", Nom = "Dupont", Prenom = "Bob", Email = "bob.dupont@erp.demo", Telephone = "+33 6 23 45 67 89", Adresse = "45 Avenue des Champs, 69001 Lyon", DateNaissance = new DateTime(1995, 8, 20), DateEmbauche = new DateTime(2023, 6, 15), PosteId = 1, Status = EmployeeStatus.Active },
                new Employe { Id = 3, Matricule = "EMP003", Nom = "Bernard", Prenom = "Claire", Email = "claire.bernard@erp.demo", Telephone = "+33 6 34 56 78 90", Adresse = "8 Boulevard Victor Hugo, 33000 Bordeaux", DateNaissance = new DateTime(1988, 3, 10), DateEmbauche = new DateTime(2019, 1, 10), PosteId = 10, Status = EmployeeStatus.Active },
                new Employe { Id = 4, Matricule = "EMP004", Nom = "Leroy", Prenom = "David", Email = "david.leroy@erp.demo", Telephone = "+33 6 45 67 89 01", Adresse = "23 Rue Gambetta, 31000 Toulouse", DateNaissance = new DateTime(1992, 11, 25), DateEmbauche = new DateTime(2021, 9, 1), PosteId = 14, Status = EmployeeStatus.Active },
                new Employe { Id = 5, Matricule = "EMP005", Nom = "Moreau", Prenom = "Emma", Email = "emma.moreau@erp.demo", Telephone = "+33 6 56 78 90 12", Adresse = "67 Rue de la Liberté, 44000 Nantes", DateNaissance = new DateTime(1997, 7, 8), DateEmbauche = new DateTime(2024, 2, 20), PosteId = 19, Status = EmployeeStatus.Active },
                new Employe { Id = 6, Matricule = "EMP006", Nom = "Petit", Prenom = "François", Email = "francois.petit@erp.demo", Telephone = "+33 6 67 89 01 23", Adresse = "34 Avenue Jean Jaurès, 13001 Marseille", DateNaissance = new DateTime(1985, 1, 30), DateEmbauche = new DateTime(2018, 5, 15), PosteId = 5, Status = EmployeeStatus.Active },
                new Employe { Id = 7, Matricule = "EMP007", Nom = "Dubois", Prenom = "Sophie", Email = "sophie.dubois@erp.demo", Telephone = "+33 6 78 90 12 34", Adresse = "56 Rue du Commerce, 59000 Lille", DateNaissance = new DateTime(1993, 9, 12), DateEmbauche = new DateTime(2020, 11, 1), PosteId = 17, Status = EmployeeStatus.OnLeave }
            };

            context.Employes.AddRange(employees);
            context.SaveChanges();
        }

        private static void SeedJobOffers(AppDbContext context)
        {
            if (context.JobOffers.Any()) return;

            var jobOffers = new List<JobOffer>
            {
                new JobOffer
                {
                    Id = 1,
                    Title = "Développeur Full Stack Junior",
                    Description = "Nous recherchons un développeur passionné pour rejoindre notre équipe IT.",
                    PosteId = 1,
                    Location = "Paris, France",
                    ContractType = ContractType.CDI,
                    SalaryMin = 35000,
                    SalaryMax = 45000,
                    PublishedDate = DateTime.Now.AddDays(-15),
                    ClosingDate = DateTime.Now.AddDays(15),
                    Status = JobOfferStatus.Published,
                    Requirements = "- 1-2 ans d'expérience\n- Connaissance de C#, .NET\n- Bases en React ou Vue.js"
                },
                new JobOffer
                {
                    Id = 2,
                    Title = "Responsable RH",
                    Description = "Poste stratégique pour gérer l'ensemble des ressources humaines.",
                    PosteId = 10,
                    Location = "Lyon, France",
                    ContractType = ContractType.CDI,
                    SalaryMin = 50000,
                    SalaryMax = 65000,
                    PublishedDate = DateTime.Now.AddDays(-7),
                    ClosingDate = DateTime.Now.AddDays(30),
                    Status = JobOfferStatus.Published,
                    Requirements = "- 5+ ans d'expérience en RH\n- Maîtrise du droit du travail\n- Leadership"
                }
            };

            context.JobOffers.AddRange(jobOffers);
            context.SaveChanges();
        }

        private static void SeedCandidatesAndCandidatures(AppDbContext context)
        {
            if (context.Candidats.Any()) return;

            // Create candidates
            var candidats = new List<Candidat>
            {
                new Candidat
                {
                    Id = 1,
                    Nom = "Rousseau",
                    Prenom = "Lucas",
                    Email = "lucas.rousseau@email.com",
                    Telephone = "+33 6 11 22 33 44",
                    DateNaissance = new DateTime(1996, 4, 12),
                    Adresse = "15 Rue des Lilas, 75011 Paris",
                    Ville = "Paris",
                    NiveauEtudes = "Bac+5 Master Informatique",
                    AnneeExperience = 2,
                    Competences = "C#, .NET, React, SQL Server"
                },
                new Candidat
                {
                    Id = 2,
                    Nom = "Girard",
                    Prenom = "Marie",
                    Email = "marie.girard@email.com",
                    Telephone = "+33 6 22 33 44 55",
                    DateNaissance = new DateTime(1994, 9, 28),
                    Adresse = "42 Avenue de la République, 69003 Lyon",
                    Ville = "Lyon",
                    NiveauEtudes = "Bac+5 École d'ingénieur",
                    AnneeExperience = 3,
                    Competences = "Java, Spring Boot, Angular, PostgreSQL"
                },
                new Candidat
                {
                    Id = 3,
                    Nom = "Lefebvre",
                    Prenom = "Antoine",
                    Email = "antoine.lefebvre@email.com",
                    Telephone = "+33 6 33 44 55 66",
                    DateNaissance = new DateTime(1991, 12, 5),
                    Adresse = "8 Boulevard Pasteur, 33000 Bordeaux",
                    Ville = "Bordeaux",
                    NiveauEtudes = "Bac+5 Master RH",
                    AnneeExperience = 10,
                    Competences = "Gestion RH, Droit du travail, Paie, SIRH"
                },
                new Candidat
                {
                    Id = 4,
                    Nom = "Fournier",
                    Prenom = "Julie",
                    Email = "julie.fournier@email.com",
                    Telephone = "+33 6 44 55 66 77",
                    DateNaissance = new DateTime(1998, 6, 18),
                    Adresse = "27 Rue Victor Hugo, 31000 Toulouse",
                    Ville = "Toulouse",
                    NiveauEtudes = "Bac+3 Licence Informatique",
                    AnneeExperience = 0,
                    Competences = "Python, JavaScript, Git"
                }
            };

            context.Candidats.AddRange(candidats);
            context.SaveChanges();

            // Create candidatures at different stages
            var candidatures = new List<Candidature>
            {
                // Lucas - Accepted, ready for hiring pipeline (demo highlight!)
                new Candidature
                {
                    Id = 1,
                    CandidatId = 1,
                    JobOfferId = 1, // Full Stack Junior
                    DateCandidature = DateTime.Now.AddDays(-10),
                    Status = CandidatureStatus.Accepted,
                    NotesInternes = "Excellent profil technique. A passé tous les entretiens avec brio. Prêt pour l'offre.",
                    LettreMotivation = "Passionné par le développement web, je souhaite rejoindre votre équipe.",
                    PretentionSalariale = 38000,
                    DateDisponibilite = DateTime.Now.AddDays(30),
                    DateEntretien = DateTime.Now.AddDays(-5),
                    RetourEntretien = "Très bon entretien technique. Motivation évidente.",
                    Score = 85
                },
                // Marie - Interview stage
                new Candidature
                {
                    Id = 2,
                    CandidatId = 2,
                    JobOfferId = 1, // Full Stack Junior
                    DateCandidature = DateTime.Now.AddDays(-7),
                    Status = CandidatureStatus.Interview,
                    NotesInternes = "Profil intéressant. Entretien technique prévu.",
                    LettreMotivation = "Forte de 3 ans d'expérience, je suis prête à relever de nouveaux défis.",
                    PretentionSalariale = 42000,
                    DateDisponibilite = DateTime.Now.AddDays(15),
                    DateEntretien = DateTime.Now.AddDays(2)
                },
                // Antoine - Accepted for HR position
                new Candidature
                {
                    Id = 3,
                    CandidatId = 3,
                    JobOfferId = 2, // Responsable RH
                    DateCandidature = DateTime.Now.AddDays(-14),
                    Status = CandidatureStatus.Accepted,
                    NotesInternes = "10 ans d'expérience en RH. Candidat idéal pour le poste.",
                    LettreMotivation = "Expert en gestion RH, je souhaite mettre mon expérience au service de votre entreprise.",
                    PretentionSalariale = 55000,
                    DateDisponibilite = DateTime.Now.AddDays(60),
                    DateEntretien = DateTime.Now.AddDays(-7),
                    RetourEntretien = "Profil senior très impressionnant. À embaucher rapidement.",
                    Score = 92
                },
                // Julie - Under review
                new Candidature
                {
                    Id = 4,
                    CandidatId = 4,
                    JobOfferId = 1, // Full Stack Junior
                    DateCandidature = DateTime.Now.AddDays(-3),
                    Status = CandidatureStatus.UnderReview,
                    NotesInternes = "Profil junior à examiner.",
                    LettreMotivation = "Diplômée en informatique, je cherche ma première opportunité professionnelle.",
                    PretentionSalariale = 32000,
                    DateDisponibilite = DateTime.Now.AddDays(7)
                }
            };

            context.Candidatures.AddRange(candidatures);
            context.SaveChanges();
        }
    }
}
