using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "RoomPrice",
                table: "AccommodationRooms",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "AccommodationRooms",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "AccommodationRooms",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsEmpty",
                table: "AccommodationRooms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationRooms_RoomNumber",
                table: "AccommodationRooms",
                column: "RoomNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccommodationRooms_RoomNumber",
                table: "AccommodationRooms");

            migrationBuilder.DropColumn(
                name: "IsEmpty",
                table: "AccommodationRooms");

            migrationBuilder.AlterColumn<string>(
                name: "RoomPrice",
                table: "AccommodationRooms",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "RoomNumber",
                table: "AccommodationRooms",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "AccommodationRooms",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
