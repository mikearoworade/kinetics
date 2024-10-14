using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kinetics.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employeeProperties_Employees_EmployeeID",
                table: "employeeProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employeeProperties",
                table: "employeeProperties");

            migrationBuilder.RenameTable(
                name: "employeeProperties",
                newName: "EmployeeProperties");

            migrationBuilder.RenameIndex(
                name: "IX_employeeProperties_EmployeeID",
                table: "EmployeeProperties",
                newName: "IX_EmployeeProperties_EmployeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProperties",
                table: "EmployeeProperties",
                column: "PropertyID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProperties_Employees_EmployeeID",
                table: "EmployeeProperties",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProperties_Employees_EmployeeID",
                table: "EmployeeProperties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProperties",
                table: "EmployeeProperties");

            migrationBuilder.RenameTable(
                name: "EmployeeProperties",
                newName: "employeeProperties");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProperties_EmployeeID",
                table: "employeeProperties",
                newName: "IX_employeeProperties_EmployeeID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employeeProperties",
                table: "employeeProperties",
                column: "PropertyID");

            migrationBuilder.AddForeignKey(
                name: "FK_employeeProperties_Employees_EmployeeID",
                table: "employeeProperties",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
