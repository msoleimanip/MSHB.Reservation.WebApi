using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SystemCodeSequence",
                startValue: 1000L);

            migrationBuilder.AlterColumn<long>(
                name: "SystemCode",
                table: "AccommodationUserRooms",
                nullable: false,
                defaultValueSql: "NEXT VALUE FOR SystemCodeSequence",
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "AccommodationUserRooms",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<long>(
                name: "CityId",
                table: "AccommodationUserRooms",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationUserRooms_CityId",
                table: "AccommodationUserRooms",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationUserRooms_City_T_CityId",
                table: "AccommodationUserRooms",
                column: "CityId",
                principalTable: "City_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationUserRooms_City_T_CityId",
                table: "AccommodationUserRooms");

            migrationBuilder.DropIndex(
                name: "IX_AccommodationUserRooms_CityId",
                table: "AccommodationUserRooms");

            migrationBuilder.DropSequence(
                name: "SystemCodeSequence");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "AccommodationUserRooms");

            migrationBuilder.AlterColumn<Guid>(
                name: "SystemCode",
                table: "AccommodationUserRooms",
                nullable: true,
                oldClrType: typeof(long),
                oldDefaultValueSql: "NEXT VALUE FOR SystemCodeSequence");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastUpdateDate",
                table: "AccommodationUserRooms",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
