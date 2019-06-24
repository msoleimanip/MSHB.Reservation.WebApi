using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IsPresident",
                table: "User_T",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "City_T",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "City_T",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "City_T");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "City_T");

            migrationBuilder.AlterColumn<int>(
                name: "IsPresident",
                table: "User_T",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
