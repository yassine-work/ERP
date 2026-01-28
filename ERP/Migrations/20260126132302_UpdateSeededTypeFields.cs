using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeededTypeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 1,
                column: "Description",
                value: "Assurance santé complémentaire pour l'employé et sa famille");

            migrationBuilder.UpdateData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 2,
                column: "Description",
                value: "Accès à une salle de sport pour la santé et le bien-être");

            migrationBuilder.UpdateData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 3,
                columns: new[] { "Description", "IsTaxable" },
                values: new object[] { "Mise à disposition d'un véhicule de fonction", true });

            migrationBuilder.UpdateData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 1,
                columns: new[] { "Description", "EligibilityRule", "IsTaxable", "Provider" },
                values: new object[] { "Allocation pour les frais de transport domicile-travail", "Tous les employés", false, "RATP / Transports publics" });

            migrationBuilder.UpdateData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 2,
                columns: new[] { "Description", "EligibilityRule", "IsTaxable", "Provider" },
                values: new object[] { "Allocation pour les repas et restauration", "Tous les employés en CDI", false, "Restaurant d'entreprise" });

            migrationBuilder.UpdateData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 3,
                columns: new[] { "Description", "EligibilityRule", "Provider" },
                values: new object[] { "Allocation pour les frais de logement", "Sur demande et justification de domicile", "Auto-géré" });

            migrationBuilder.UpdateData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 4,
                columns: new[] { "Description", "EligibilityRule", "IsTaxable", "Provider" },
                values: new object[] { "Allocation pour usage professionnel du téléphone", "Employés en contact client ou télétravail", false, "Forfait opérateur" });

            migrationBuilder.UpdateData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 1,
                columns: new[] { "BonusRule", "Description" },
                values: new object[] { "Basée sur les KPIs individuels, entre 0-20% du salaire mensuel", "Prime basée sur la performance et les objectifs atteints" });

            migrationBuilder.UpdateData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 2,
                columns: new[] { "BonusRule", "Description" },
                values: new object[] { "500-2000€ selon le poste recruté et la durée de permanence", "Prime pour recommandation et recrutement de candidats" });

            migrationBuilder.UpdateData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 3,
                columns: new[] { "BonusRule", "Description", "IsAutomatic" },
                values: new object[] { "1 mois de salaire minimum, variable selon ancienneté", "Prime de fin d'année versée en décembre", true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 1,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 2,
                column: "Description",
                value: null);

            migrationBuilder.UpdateData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 3,
                columns: new[] { "Description", "IsTaxable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 1,
                columns: new[] { "Description", "EligibilityRule", "IsTaxable", "Provider" },
                values: new object[] { null, null, true, null });

            migrationBuilder.UpdateData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 2,
                columns: new[] { "Description", "EligibilityRule", "IsTaxable", "Provider" },
                values: new object[] { null, null, true, null });

            migrationBuilder.UpdateData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 3,
                columns: new[] { "Description", "EligibilityRule", "Provider" },
                values: new object[] { null, null, null });

            migrationBuilder.UpdateData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 4,
                columns: new[] { "Description", "EligibilityRule", "IsTaxable", "Provider" },
                values: new object[] { null, null, true, null });

            migrationBuilder.UpdateData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 1,
                columns: new[] { "BonusRule", "Description" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 2,
                columns: new[] { "BonusRule", "Description" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 3,
                columns: new[] { "BonusRule", "Description", "IsAutomatic" },
                values: new object[] { null, null, false });
        }
    }
}
