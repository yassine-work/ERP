using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class TablesPosteAdvantagesBonusesAllowancesCompensation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poste",
                table: "Employes");

            migrationBuilder.AddColumn<int>(
                name: "PosteId",
                table: "Employes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdvantageTypes",
                columns: table => new
                {
                    AdvantageTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdvantageTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvantageTypes", x => x.AdvantageTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AllowanceTypes",
                columns: table => new
                {
                    AllowanceTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllowanceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowanceTypes", x => x.AllowanceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "BonusTypes",
                columns: table => new
                {
                    BonusTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BonusTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusTypes", x => x.BonusTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CompensationPackages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    BaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EffectiveFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EffectiveTo = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                name: "Postes",
                columns: table => new
                {
                    PosteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MinimumBaseSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postes", x => x.PosteId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAdvantages",
                columns: table => new
                {
                    EmployeeAdvantageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompensationPackageId = table.Column<int>(type: "int", nullable: false),
                    AdvantageTypeId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EligibilityRule = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    EmployeeAllowanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompensationPackageId = table.Column<int>(type: "int", nullable: false),
                    AllowanceTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsRecurring = table.Column<bool>(type: "bit", nullable: false),
                    IsTaxable = table.Column<bool>(type: "bit", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
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
                    EmployeeBonusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompensationPackageId = table.Column<int>(type: "int", nullable: false),
                    BonusTypeId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAutomatic = table.Column<bool>(type: "bit", nullable: false),
                    IsTaxable = table.Column<bool>(type: "bit", nullable: false),
                    IsExceptional = table.Column<bool>(type: "bit", nullable: false),
                    IsPerformanceBased = table.Column<bool>(type: "bit", nullable: false),
                    BonusRule = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_Employes_PosteId",
                table: "Employes",
                column: "PosteId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Employes_Postes_PosteId",
                table: "Employes",
                column: "PosteId",
                principalTable: "Postes",
                principalColumn: "PosteId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employes_Postes_PosteId",
                table: "Employes");

            migrationBuilder.DropTable(
                name: "EmployeeAdvantages");

            migrationBuilder.DropTable(
                name: "EmployeeAllowances");

            migrationBuilder.DropTable(
                name: "EmployeeBonuses");

            migrationBuilder.DropTable(
                name: "Postes");

            migrationBuilder.DropTable(
                name: "AdvantageTypes");

            migrationBuilder.DropTable(
                name: "AllowanceTypes");

            migrationBuilder.DropTable(
                name: "BonusTypes");

            migrationBuilder.DropTable(
                name: "CompensationPackages");

            migrationBuilder.DropIndex(
                name: "IX_Employes_PosteId",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "PosteId",
                table: "Employes");

            migrationBuilder.AddColumn<string>(
                name: "Poste",
                table: "Employes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
