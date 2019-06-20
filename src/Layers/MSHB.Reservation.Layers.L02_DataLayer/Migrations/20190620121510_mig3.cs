using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmpty",
                table: "AccommodationRooms");

            migrationBuilder.AlterColumn<Guid>(
                name: "SystemCode",
                table: "AccommodationUserRooms",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SystemCode",
                table: "AccommodationUserRooms",
                nullable: true,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmpty",
                table: "AccommodationRooms",
                nullable: true);
        }
    }
}
