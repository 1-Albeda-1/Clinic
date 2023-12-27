using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinic.Context.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Сomplaint",
                table: "BookingAppointments",
                newName: "Complaint");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Complaint",
                table: "BookingAppointments",
                newName: "Сomplaint");
        }
    }
}
