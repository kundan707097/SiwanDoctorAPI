using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiwanDoctorAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableFamilyMemberVitals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Family_MemberVitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKpatientDetails = table.Column<int>(name: "FK_patient_Details", type: "int", nullable: false),
                    FKuserFamilyMember = table.Column<int>(name: "FK_userFamilyMember", type: "int", nullable: false),
                    BpSystolic = table.Column<int>(type: "int", nullable: false),
                    BpDiastolic = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Spo2 = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    SugarRandom = table.Column<int>(type: "int", nullable: false),
                    SugarFasting = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Family_MemberVitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Family_MemberVitals_User_FamilyMember_FK_userFamilyMember",
                        column: x => x.FKuserFamilyMember,
                        principalTable: "User_FamilyMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Family_MemberVitals_FK_userFamilyMember",
                table: "Family_MemberVitals",
                column: "FK_userFamilyMember");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Family_MemberVitals");
        }
    }
}
