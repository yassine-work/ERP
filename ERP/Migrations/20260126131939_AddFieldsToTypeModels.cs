using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldsToTypeModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BonusRule",
                table: "BonusTypes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BonusTypes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAutomatic",
                table: "BonusTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxable",
                table: "BonusTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AllowanceTypes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EligibilityRule",
                table: "AllowanceTypes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxable",
                table: "AllowanceTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "AllowanceTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AdvantageTypes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsTaxable",
                table: "AdvantageTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 1,
                columns: new[] { "Description", "IsTaxable" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "AdvantageTypes",
                keyColumn: "AdvantageTypeId",
                keyValue: 2,
                columns: new[] { "Description", "IsTaxable" },
                values: new object[] { null, false });

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
                columns: new[] { "Description", "EligibilityRule", "IsTaxable", "Provider" },
                values: new object[] { null, null, true, null });

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
                columns: new[] { "BonusRule", "Description", "IsAutomatic", "IsTaxable" },
                values: new object[] { null, null, false, true });

            migrationBuilder.UpdateData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 2,
                columns: new[] { "BonusRule", "Description", "IsAutomatic", "IsTaxable" },
                values: new object[] { null, null, false, true });

            migrationBuilder.UpdateData(
                table: "BonusTypes",
                keyColumn: "BonusTypeId",
                keyValue: 3,
                columns: new[] { "BonusRule", "Description", "IsAutomatic", "IsTaxable" },
                values: new object[] { null, null, false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BonusRule",
                table: "BonusTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BonusTypes");

            migrationBuilder.DropColumn(
                name: "IsAutomatic",
                table: "BonusTypes");

            migrationBuilder.DropColumn(
                name: "IsTaxable",
                table: "BonusTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AllowanceTypes");

            migrationBuilder.DropColumn(
                name: "EligibilityRule",
                table: "AllowanceTypes");

            migrationBuilder.DropColumn(
                name: "IsTaxable",
                table: "AllowanceTypes");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "AllowanceTypes");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AdvantageTypes");

            migrationBuilder.DropColumn(
                name: "IsTaxable",
                table: "AdvantageTypes");
        }
    }
}
