using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ReserveCode",
                table: "Bookination_T",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReserveCode",
                table: "Bookination_T");
        }
    }
}
