using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiwanDoctorAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTablePatientPrescriptionAndPatientMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patient_Prescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    UserFamilyMemberId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Test = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Advice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProblemDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FoodAllergies = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TendencyBleed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeartDisease = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diabetic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Surgery = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Accident = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentMedication = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FemalePregnancy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BreastFeeding = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PulseRate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextVisit = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Prescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Prescription_Doctor_Information_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctor_Information",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_Prescription_Patient_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Patient_Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patient_Prescription_User_FamilyMember_UserFamilyMemberId",
                        column: x => x.UserFamilyMemberId,
                        principalTable: "User_FamilyMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patient_Medicine",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoseInterval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPrescriptionId = table.Column<int>(type: "int", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Medicine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_Medicine_Patient_Prescription_PatientPrescriptionId",
                        column: x => x.PatientPrescriptionId,
                        principalTable: "Patient_Prescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Medicine_PatientPrescriptionId",
                table: "Patient_Medicine",
                column: "PatientPrescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Prescription_AppointmentId",
                table: "Patient_Prescription",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Prescription_DoctorId",
                table: "Patient_Prescription",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Prescription_UserFamilyMemberId",
                table: "Patient_Prescription",
                column: "UserFamilyMemberId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patient_Medicine");

            migrationBuilder.DropTable(
                name: "Patient_Prescription");
        }
    }
}
