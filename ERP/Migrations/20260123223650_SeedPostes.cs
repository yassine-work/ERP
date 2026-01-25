using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ERP.Migrations
{
    /// <inheritdoc />
    public partial class SeedPostes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Postes",
                keyColumn: "PosteId",
                keyValue: 28);
        }
    }
}
