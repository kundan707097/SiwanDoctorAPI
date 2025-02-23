using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiwanDoctorAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnstopbooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "StopBooking",
                table: "Doctor_Information",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StopBooking",
                table: "Doctor_Information");
        }
    }
}
