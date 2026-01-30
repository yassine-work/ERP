using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EligibilityRule",
                table: "EmployeeAdvantages");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "EmployeeAdvantages");

            migrationBuilder.AddColumn<DateTime>(
                name: "AwardedOn",
                table: "EmployeeBonuses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidUntil",
                table: "EmployeeBonuses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveFrom",
                table: "EmployeeAllowances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EffectiveTo",
                table: "EmployeeAllowances",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EligibilityRule",
                table: "AdvantageTypes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "AdvantageTypes",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwardedOn",
                table: "EmployeeBonuses");

            migrationBuilder.DropColumn(
                name: "ValidUntil",
                table: "EmployeeBonuses");

            migrationBuilder.DropColumn(
                name: "EffectiveFrom",
                table: "EmployeeAllowances");

            migrationBuilder.DropColumn(
                name: "EffectiveTo",
                table: "EmployeeAllowances");

            migrationBuilder.DropColumn(
                name: "EligibilityRule",
                table: "AdvantageTypes");

            migrationBuilder.DropColumn(
                name: "Provider",
                table: "AdvantageTypes");

            migrationBuilder.AddColumn<string>(
                name: "EligibilityRule",
                table: "EmployeeAdvantages",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Provider",
                table: "EmployeeAdvantages",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
