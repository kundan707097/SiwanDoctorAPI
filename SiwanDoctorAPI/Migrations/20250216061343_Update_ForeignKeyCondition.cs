using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiwanDoctorAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKeyCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Appointments_Patient_Information_FK_PatientId",
                table: "Patient_Appointments");

            migrationBuilder.RenameColumn(
                name: "FK_PatientId",
                table: "Patient_Appointments",
                newName: "UserFamilyMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_Appointments_FK_PatientId",
                table: "Patient_Appointments",
                newName: "IX_Patient_Appointments_UserFamilyMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Appointments_UserId",
                table: "Patient_Appointments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Appointments_Patient_Information_UserId",
                table: "Patient_Appointments",
                column: "UserId",
                principalTable: "Patient_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Appointments_User_FamilyMember_UserFamilyMemberId",
                table: "Patient_Appointments",
                column: "UserFamilyMemberId",
                principalTable: "User_FamilyMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Appointments_Patient_Information_UserId",
                table: "Patient_Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Appointments_User_FamilyMember_UserFamilyMemberId",
                table: "Patient_Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Patient_Appointments_UserId",
                table: "Patient_Appointments");

            migrationBuilder.RenameColumn(
                name: "UserFamilyMemberId",
                table: "Patient_Appointments",
                newName: "FK_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_Appointments_UserFamilyMemberId",
                table: "Patient_Appointments",
                newName: "IX_Patient_Appointments_FK_PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Appointments_Patient_Information_FK_PatientId",
                table: "Patient_Appointments",
                column: "FK_PatientId",
                principalTable: "Patient_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
