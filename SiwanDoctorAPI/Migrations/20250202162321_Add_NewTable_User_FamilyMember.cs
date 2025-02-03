using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiwanDoctorAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTableUserFamilyMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User_FamilyMember",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(name: "F_Name", type: "nvarchar(max)", nullable: true),
                    LName = table.Column<string>(name: "L_Name", type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsdCode = table.Column<string>(name: "Isd_Code", type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(name: "User_Id", type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_User_FamilyMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_FamilyMember_Patient_Information_User_Id",
                        column: x => x.UserId,
                        principalTable: "Patient_Information",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_FamilyMember_User_Id",
                table: "User_FamilyMember",
                column: "User_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User_FamilyMember");
        }
    }
}
