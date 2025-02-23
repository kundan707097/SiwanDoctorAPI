using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiwanDoctorAPI.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertiesDepartmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Information_Doctor_Department_DeptId",
                table: "Doctor_Information");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Doctor_Information");

            migrationBuilder.RenameColumn(
                name: "DeptId",
                table: "Doctor_Information",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctor_Information_DeptId",
                table: "Doctor_Information",
                newName: "IX_Doctor_Information_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Information_Doctor_Department_DepartmentId",
                table: "Doctor_Information",
                column: "DepartmentId",
                principalTable: "Doctor_Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Information_Doctor_Department_DepartmentId",
                table: "Doctor_Information");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Doctor_Information",
                newName: "DeptId");

            migrationBuilder.RenameIndex(
                name: "IX_Doctor_Information_DepartmentId",
                table: "Doctor_Information",
                newName: "IX_Doctor_Information_DeptId");

            migrationBuilder.AddColumn<int>(
                name: "Department",
                table: "Doctor_Information",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Information_Doctor_Department_DeptId",
                table: "Doctor_Information",
                column: "DeptId",
                principalTable: "Doctor_Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
