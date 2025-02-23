using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiwanDoctorAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnAndPassForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Department",
                table: "Doctor_Information",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ClinicAppointment",
                table: "Doctor_Information",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Doctor_Information",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmergencyAppointment",
                table: "Doctor_Information",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "VideoAppointment",
                table: "Doctor_Information",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_Information_DeptId",
                table: "Doctor_Information",
                column: "DeptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Information_Doctor_Department_DeptId",
                table: "Doctor_Information",
                column: "DeptId",
                principalTable: "Doctor_Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Information_Doctor_Department_DeptId",
                table: "Doctor_Information");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_Information_DeptId",
                table: "Doctor_Information");

            migrationBuilder.DropColumn(
                name: "ClinicAppointment",
                table: "Doctor_Information");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Doctor_Information");

            migrationBuilder.DropColumn(
                name: "EmergencyAppointment",
                table: "Doctor_Information");

            migrationBuilder.DropColumn(
                name: "VideoAppointment",
                table: "Doctor_Information");

            migrationBuilder.AlterColumn<string>(
                name: "Department",
                table: "Doctor_Information",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
