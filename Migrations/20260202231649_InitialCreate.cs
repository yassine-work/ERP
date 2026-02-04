using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvantageTypes",
                columns: table => new
                {
                    AdvantageTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AdvantageTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Provider = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EligibilityRule = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsTaxable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvantageTypes", x => x.AdvantageTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AllowanceTypes",
                columns: table => new
                {
                    AllowanceTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AllowanceTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Provider = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EligibilityRule = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsTaxable = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceTypes", x => x.AllowanceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BonusTypes",
                columns: table => new
                {
                    BonusTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BonusTypeName = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    BonusRule = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IsTaxable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsAutomatic = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusTypes", x => x.BonusTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentTypes",
                columns: table => new
                {
                    EquipmentTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Category = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    EstimatedLifespanMonths = table.Column<int>(type: "INTEGER", nullable: true),
                    RequiresMaintenance = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentTypes", x => x.EquipmentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Postes",
                columns: table => new
                {
                    PosteId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    MinimumBaseSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    Department = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postes", x => x.PosteId);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    EquipmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    SerialNumber = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    InventoryCode = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    EquipmentTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Brand = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    AcquisitionDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "TEXT", nullable: true),
                    Supplier = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    WarrantyEndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.EquipmentId);
                    table.ForeignKey(
                        name: "FK_Equipments_EquipmentTypes_EquipmentTypeId",
                        column: x => x.EquipmentTypeId,
                        principalTable: "EquipmentTypes",
                        principalColumn: "EquipmentTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Matricule = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Nom = table.Column<string>(type: "TEXT", nullable: false),
                    Prenom = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: true),
                    DateNaissance = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LieuNaissance = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CIN = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Nationalite = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    SituationFamiliale = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    NombreEnfants = table.Column<int>(type: "INTEGER", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Telephone = table.Column<string>(type: "TEXT", nullable: false),
                    TelephoneUrgence = table.Column<string>(type: "TEXT", nullable: true),
                    ContactUrgence = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Adresse = table.Column<string>(type: "TEXT", nullable: false),
                    Ville = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    CodePostal = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    PosteId = table.Column<int>(type: "INTEGER", nullable: false),
                    DateEmbauche = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateFinContrat = table.Column<DateTime>(type: "TEXT", nullable: true),
                    TypeContrat = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    PhotoUrl = table.Column<string>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employes_Postes_PosteId",
                        column: x => x.PosteId,
                        principalTable: "Postes",
                        principalColumn: "PosteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompensationPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompensationPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompensationPackages_Employes_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EquipmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    AssignmentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ConditionAtAssignment = table.Column<int>(type: "INTEGER", nullable: false),
                    ConditionAtReturn = table.Column<int>(type: "INTEGER", nullable: true),
                    AssignedBy = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    AssignmentNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    ReturnNotes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentAssignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_EquipmentAssignments_Employes_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EquipmentAssignments_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "EquipmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAdvantages",
                columns: table => new
                {
                    EmployeeAdvantageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompensationPackageId = table.Column<int>(type: "INTEGER", nullable: false),
                    AdvantageTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAdvantages", x => x.EmployeeAdvantageId);
                    table.ForeignKey(
                        name: "FK_EmployeeAdvantages_AdvantageTypes_AdvantageTypeId",
                        column: x => x.AdvantageTypeId,
                        principalTable: "AdvantageTypes",
                        principalColumn: "AdvantageTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAdvantages_CompensationPackages_CompensationPackageId",
                        column: x => x.CompensationPackageId,
                        principalTable: "CompensationPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAllowances",
                columns: table => new
                {
                    EmployeeAllowanceId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompensationPackageId = table.Column<int>(type: "INTEGER", nullable: false),
                    AllowanceTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsRecurring = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTaxable = table.Column<bool>(type: "INTEGER", nullable: false),
                    Frequency = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    EffectiveFrom = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAllowances", x => x.EmployeeAllowanceId);
                    table.ForeignKey(
                        name: "FK_EmployeeAllowances_AllowanceTypes_AllowanceTypeId",
                        column: x => x.AllowanceTypeId,
                        principalTable: "AllowanceTypes",
                        principalColumn: "AllowanceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAllowances_CompensationPackages_CompensationPackageId",
                        column: x => x.CompensationPackageId,
                        principalTable: "CompensationPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBonuses",
                columns: table => new
                {
                    EmployeeBonusId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompensationPackageId = table.Column<int>(type: "INTEGER", nullable: false),
                    BonusTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsAutomatic = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsTaxable = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsExceptional = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsPerformanceBased = table.Column<bool>(type: "INTEGER", nullable: false),
                    BonusRule = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    AwardedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBonuses", x => x.EmployeeBonusId);
                    table.ForeignKey(
                        name: "FK_EmployeeBonuses_BonusTypes_BonusTypeId",
                        column: x => x.BonusTypeId,
                        principalTable: "BonusTypes",
                        principalColumn: "BonusTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeBonuses_CompensationPackages_CompensationPackageId",
                        column: x => x.CompensationPackageId,
                        principalTable: "CompensationPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AdvantageTypes",
                columns: new[] { "AdvantageTypeId", "AdvantageTypeName", "Description", "EligibilityRule", "IsTaxable", "Provider" },
                values: new object[,]
                {
                    { 1, "Health Insurance", "Assurance santé complémentaire pour l'employé et sa famille", "Full-time employees only", false, "Global Health" },
                    { 2, "Gym Membership", "Accès à une salle de sport pour la santé et le bien-être", "Available after 3 months of employment", false, "FitClub" },
                    { 3, "Company Car", "Mise à disposition d'un véhicule de fonction", "Managers only", true, "Leasing Co." }
                });

            migrationBuilder.InsertData(
                table: "AllowanceTypes",
                columns: new[] { "AllowanceTypeId", "AllowanceTypeName", "Description", "EligibilityRule", "IsTaxable", "Provider" },
                values: new object[,]
                {
                    { 1, "Transport", "Allocation pour les frais de transport domicile-travail", "Tous les employés", false, "RATP / Transports publics" },
                    { 2, "Meal", "Allocation pour les repas et restauration", "Tous les employés en CDI", false, "Restaurant d'entreprise" },
                    { 3, "Housing", "Allocation pour les frais de logement", "Sur demande et justification de domicile", true, "Auto-géré" },
                    { 4, "Phone", "Allocation pour usage professionnel du téléphone", "Employés en contact client ou télétravail", false, "Forfait opérateur" }
                });

            migrationBuilder.InsertData(
                table: "BonusTypes",
                columns: new[] { "BonusTypeId", "BonusRule", "BonusTypeName", "Description", "IsAutomatic", "IsTaxable" },
                values: new object[,]
                {
                    { 1, "Basée sur les KPIs individuels, entre 0-20% du salaire mensuel", "Performance", "Prime basée sur la performance et les objectifs atteints", false, true },
                    { 2, "500-2000€ selon le poste recruté et la durée de permanence", "Referral", "Prime pour recommandation et recrutement de candidats", false, true },
                    { 3, "1 mois de salaire minimum, variable selon ancienneté", "Year-end", "Prime de fin d'année versée en décembre", true, true }
                });

            migrationBuilder.InsertData(
                table: "EquipmentTypes",
                columns: new[] { "EquipmentTypeId", "Category", "Description", "EstimatedLifespanMonths", "Name", "RequiresMaintenance" },
                values: new object[,]
                {
                    { 1, "Informatique", "Ordinateur portable professionnel pour le travail", 48, "Ordinateur Portable", true },
                    { 2, "Informatique", "Ordinateur fixe avec écran pour le bureau", 60, "Ordinateur de Bureau", true },
                    { 3, "Informatique", "Smartphone professionnel", 24, "Téléphone Mobile", false },
                    { 4, "Sécurité", "Badge d'accès aux locaux de l'entreprise", null, "Badge d'accès", false },
                    { 5, "Sécurité", "Trousseau de clés pour accès aux bureaux", null, "Clés de bureau", false },
                    { 6, "Vêtements", "Uniforme ou vêtements de travail fournis", 12, "Tenue de travail", false },
                    { 7, "Sécurité", "Casque, gants, lunettes de protection, etc.", 12, "Équipement de sécurité", false },
                    { 8, "Mobilier", "Bureau, chaise ergonomique, etc.", 120, "Mobilier de bureau", false },
                    { 9, "Transport", "Voiture ou véhicule de fonction", 60, "Véhicule de service", true },
                    { 10, "Informatique", "Moniteur additionnel pour le poste de travail", 72, "Écran supplémentaire", false }
                });

            migrationBuilder.InsertData(
                table: "Postes",
                columns: new[] { "PosteId", "Department", "MinimumBaseSalary", "Title" },
                values: new object[,]
                {
                    { 1, "IT", 2500m, "Junior Developer" },
                    { 2, "IT", 5000m, "Senior Developer" },
                    { 3, "IT", 4000m, "Full Stack Developer" },
                    { 4, "IT", 4500m, "DevOps Engineer" },
                    { 5, "IT", 7000m, "IT Manager" },
                    { 6, "IT", 3500m, "System Administrator" },
                    { 7, "IT", 3000m, "QA Engineer" },
                    { 8, "Human Resources", 3000m, "HR Specialist" },
                    { 9, "Human Resources", 2800m, "Recruiter" },
                    { 10, "Human Resources", 5500m, "HR Manager" },
                    { 11, "Human Resources", 3200m, "Training Coordinator" },
                    { 12, "Human Resources", 3000m, "Payroll Specialist" },
                    { 13, "Finance", 3200m, "Accountant" },
                    { 14, "Finance", 4000m, "Financial Analyst" },
                    { 15, "Finance", 4500m, "Senior Accountant" },
                    { 16, "Finance", 6000m, "Finance Manager" },
                    { 17, "Sales", 2500m, "Sales Representative" },
                    { 18, "Sales", 5000m, "Sales Manager" },
                    { 19, "Marketing", 3200m, "Marketing Specialist" },
                    { 20, "Marketing", 4500m, "Digital Marketing Manager" },
                    { 21, "Marketing", 2800m, "Content Creator" },
                    { 22, "Operations", 5500m, "Operations Manager" },
                    { 23, "Operations", 4500m, "Project Manager" },
                    { 24, "Operations", 2200m, "Administrative Assistant" },
                    { 25, "Operations", 3800m, "Business Analyst" },
                    { 26, "Customer Support", 2400m, "Customer Support Agent" },
                    { 27, "Customer Support", 4200m, "Customer Support Manager" },
                    { 28, "Customer Support", 3000m, "Technical Support Specialist" }
                });

            migrationBuilder.InsertData(
                table: "Equipments",
                columns: new[] { "EquipmentId", "AcquisitionDate", "Brand", "EquipmentTypeId", "InventoryCode", "Model", "Name", "Notes", "PurchasePrice", "SerialNumber", "Status", "Supplier", "WarrantyEndDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 1, "LAP-001", "MacBook Pro 14\" M3", "MacBook Pro 14\"", null, 2499.00m, "C02X12345678", 0, "Apple Store", new DateTime(2028, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lenovo", 1, "LAP-002", "ThinkPad X1 Carbon Gen 11", "ThinkPad X1 Carbon", null, 1899.00m, "PF2ABCD1234", 0, "Lenovo Direct", new DateTime(2028, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dell", 1, "LAP-003", "Latitude 5540", "Dell Latitude 5540", null, 1299.00m, "DELL12345ABC", 0, "Dell Business", new DateTime(2028, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dell", 2, "DES-001", "OptiPlex 7010", "Dell OptiPlex 7010", null, 899.00m, "OPTIPLEX7890", 0, "Dell Business", new DateTime(2027, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Apple", 3, "PHN-001", "iPhone 15 Pro 256GB", "iPhone 15 Pro", null, 1199.00m, "DQWERTY12345", 0, "Apple Store", new DateTime(2027, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Samsung", 3, "PHN-002", "Galaxy S24 Ultra", "Samsung Galaxy S24", null, 1099.00m, "RFCM12345678", 0, "Samsung Business", new DateTime(2027, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HID", 4, "BAD-001", "iCLASS SE", "Badge Accès Principal", null, 15.00m, "BADGE-001", 0, null, null },
                    { 8, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HID", 4, "BAD-002", "iCLASS SE", "Badge Accès Principal", null, 15.00m, "BADGE-002", 0, null, null },
                    { 9, new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dell", 10, "MON-001", "UltraSharp U2722D", "Dell UltraSharp 27\"", null, 549.00m, "MONITOR-001", 0, "Dell Business", new DateTime(2027, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2024, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "LG", 10, "MON-002", "34WN80C-B", "LG 34\" Curved", null, 699.00m, "MONITOR-002", 0, "Amazon Business", new DateTime(2027, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompensationPackages_EmployeeId",
                table: "CompensationPackages",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvantages_AdvantageTypeId",
                table: "EmployeeAdvantages",
                column: "AdvantageTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvantages_CompensationPackageId_AdvantageTypeId",
                table: "EmployeeAdvantages",
                columns: new[] { "CompensationPackageId", "AdvantageTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAllowances_AllowanceTypeId",
                table: "EmployeeAllowances",
                column: "AllowanceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAllowances_CompensationPackageId_AllowanceTypeId",
                table: "EmployeeAllowances",
                columns: new[] { "CompensationPackageId", "AllowanceTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBonuses_BonusTypeId",
                table: "EmployeeBonuses",
                column: "BonusTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBonuses_CompensationPackageId_BonusTypeId",
                table: "EmployeeBonuses",
                columns: new[] { "CompensationPackageId", "BonusTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employes_PosteId",
                table: "Employes",
                column: "PosteId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssignments_EmployeeId",
                table: "EquipmentAssignments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssignments_EquipmentId",
                table: "EquipmentAssignments",
                column: "EquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_EquipmentTypeId",
                table: "Equipments",
                column: "EquipmentTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeAdvantages");

            migrationBuilder.DropTable(
                name: "EmployeeAllowances");

            migrationBuilder.DropTable(
                name: "EmployeeBonuses");

            migrationBuilder.DropTable(
                name: "EquipmentAssignments");

            migrationBuilder.DropTable(
                name: "AdvantageTypes");

            migrationBuilder.DropTable(
                name: "AllowanceTypes");

            migrationBuilder.DropTable(
                name: "BonusTypes");

            migrationBuilder.DropTable(
                name: "CompensationPackages");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Employes");

            migrationBuilder.DropTable(
                name: "EquipmentTypes");

            migrationBuilder.DropTable(
                name: "Postes");
        }
    }
}
