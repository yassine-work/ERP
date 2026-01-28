using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class SeedAllowancesAdvantagesBonuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AdvantageTypes",
                columns: new[] { "AdvantageTypeId", "AdvantageTypeName", "EligibilityRule", "Provider" },
                values: new object[,]
                {
                    { 1, "Health Insurance", "Full-time employees only", "Global Health" },
                    { 2, "Gym Membership", "Available after 3 months of employment", "FitClub" },
                    { 3, "Company Car", "Managers only", "Leasing Co." }
                });

            migrationBuilder.InsertData(
                table: "AllowanceTypes",
                columns: new[] { "AllowanceTypeId", "AllowanceTypeName" },
                values: new object[,]
                {
                    { 1, "Transport" },
                    { 2, "Meal" },
                    { 3, "Housing" },
                    { 4, "Phone" }
                });

            migrationBuilder.InsertData(
                table: "BonusTypes",
                columns: new[] { "BonusTypeId", "BonusTypeName" },
                values: new object[,]
                {
                    { 1, "Performance" },
                    { 2, "Referral" },
                    { 3, "Year-end" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AllowanceTypes",
                keyColumn: "AllowanceTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 3);
        }
    }
}
