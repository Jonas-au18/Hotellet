using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_california.Data.Migrations
{
    public partial class modelupdae : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomNum",
                table: "Guests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomNum",
                table: "Guests");
        }
    }
}
