﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiwanDoctorAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Information_AspNetUsers_UserId",
                table: "Doctor_Information");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Reviews_Doctor_Information_FK_DoctorId",
                table: "Doctor_Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_TimeSlot_Doctor_Information_doct_id",
                table: "Doctor_TimeSlot");

            migrationBuilder.DropForeignKey(
                name: "FK_Family_MemberVitals_User_FamilyMember_FK_userFamilyMember",
                table: "Family_MemberVitals");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Appointments_Doctor_Information_FK_DoctId",
                table: "Patient_Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Information_AspNetUsers_UserId",
                table: "Patient_Information");

            migrationBuilder.DropForeignKey(
                name: "FK_User_FamilyMember_Patient_Information_User_Id",
                table: "User_FamilyMember");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoDoctor_TimeSlot_Doctor_Information_doct_id",
                table: "VideoDoctor_TimeSlot");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Appointments_FK_PatientId",
                table: "Patient_Appointments",
                column: "FK_PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Information_AspNetUsers_UserId",
                table: "Doctor_Information",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Reviews_Doctor_Information_FK_DoctorId",
                table: "Doctor_Reviews",
                column: "FK_DoctorId",
                principalTable: "Doctor_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_TimeSlot_Doctor_Information_doct_id",
                table: "Doctor_TimeSlot",
                column: "doct_id",
                principalTable: "Doctor_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Family_MemberVitals_User_FamilyMember_FK_userFamilyMember",
                table: "Family_MemberVitals",
                column: "FK_userFamilyMember",
                principalTable: "User_FamilyMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Appointments_Doctor_Information_FK_DoctId",
                table: "Patient_Appointments",
                column: "FK_DoctId",
                principalTable: "Doctor_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Appointments_Patient_Information_FK_PatientId",
                table: "Patient_Appointments",
                column: "FK_PatientId",
                principalTable: "Patient_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Information_AspNetUsers_UserId",
                table: "Patient_Information",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_FamilyMember_Patient_Information_User_Id",
                table: "User_FamilyMember",
                column: "User_Id",
                principalTable: "Patient_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoDoctor_TimeSlot_Doctor_Information_doct_id",
                table: "VideoDoctor_TimeSlot",
                column: "doct_id",
                principalTable: "Doctor_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Information_AspNetUsers_UserId",
                table: "Doctor_Information");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Reviews_Doctor_Information_FK_DoctorId",
                table: "Doctor_Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_TimeSlot_Doctor_Information_doct_id",
                table: "Doctor_TimeSlot");

            migrationBuilder.DropForeignKey(
                name: "FK_Family_MemberVitals_User_FamilyMember_FK_userFamilyMember",
                table: "Family_MemberVitals");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Appointments_Doctor_Information_FK_DoctId",
                table: "Patient_Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Appointments_Patient_Information_FK_PatientId",
                table: "Patient_Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Information_AspNetUsers_UserId",
                table: "Patient_Information");

            migrationBuilder.DropForeignKey(
                name: "FK_User_FamilyMember_Patient_Information_User_Id",
                table: "User_FamilyMember");

            migrationBuilder.DropForeignKey(
                name: "FK_VideoDoctor_TimeSlot_Doctor_Information_doct_id",
                table: "VideoDoctor_TimeSlot");

            migrationBuilder.DropIndex(
                name: "IX_Patient_Appointments_FK_PatientId",
                table: "Patient_Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Information_AspNetUsers_UserId",
                table: "Doctor_Information",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Reviews_Doctor_Information_FK_DoctorId",
                table: "Doctor_Reviews",
                column: "FK_DoctorId",
                principalTable: "Doctor_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_TimeSlot_Doctor_Information_doct_id",
                table: "Doctor_TimeSlot",
                column: "doct_id",
                principalTable: "Doctor_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Family_MemberVitals_User_FamilyMember_FK_userFamilyMember",
                table: "Family_MemberVitals",
                column: "FK_userFamilyMember",
                principalTable: "User_FamilyMember",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Appointments_Doctor_Information_FK_DoctId",
                table: "Patient_Appointments",
                column: "FK_DoctId",
                principalTable: "Doctor_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Information_AspNetUsers_UserId",
                table: "Patient_Information",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_FamilyMember_Patient_Information_User_Id",
                table: "User_FamilyMember",
                column: "User_Id",
                principalTable: "Patient_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VideoDoctor_TimeSlot_Doctor_Information_doct_id",
                table: "VideoDoctor_TimeSlot",
                column: "doct_id",
                principalTable: "Doctor_Information",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
