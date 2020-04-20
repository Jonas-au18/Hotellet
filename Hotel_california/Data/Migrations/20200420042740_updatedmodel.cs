using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_california.Data.Migrations
{
    public partial class updatedmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guests_Bookings_BookingId",
                table: "Guests");

            migrationBuilder.DropIndex(
                name: "IX_Guests_BookingId",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "Guests");

            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Kids",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Kids",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "BookingId",
                table: "Guests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Guests_BookingId",
                table: "Guests",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guests_Bookings_BookingId",
                table: "Guests",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
