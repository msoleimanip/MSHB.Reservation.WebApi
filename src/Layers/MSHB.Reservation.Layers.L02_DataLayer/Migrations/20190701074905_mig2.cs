using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AccommodationUserRooms",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "AccommodationUserRooms",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "AccommodationUserRooms");

            migrationBuilder.AlterColumn<long>(
                name: "Description",
                table: "AccommodationUserRooms",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
