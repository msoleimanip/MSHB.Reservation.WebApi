using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveryTime",
                table: "AccommodationRooms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EvacuationTime",
                table: "AccommodationRooms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageAddress",
                table: "AccommodationRooms",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccommodationAttachments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccommodationRoomId = table.Column<long>(nullable: false),
                    FileType = table.Column<string>(maxLength: 20, nullable: true),
                    FileSize = table.Column<long>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationAttachments_AccommodationRooms_AccommodationRoomId",
                        column: x => x.AccommodationRoomId,
                        principalTable: "AccommodationRooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationAttachments_AccommodationRoomId",
                table: "AccommodationAttachments",
                column: "AccommodationRoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccommodationAttachments");

            migrationBuilder.DropColumn(
                name: "DeliveryTime",
                table: "AccommodationRooms");

            migrationBuilder.DropColumn(
                name: "EvacuationTime",
                table: "AccommodationRooms");

            migrationBuilder.DropColumn(
                name: "ImageAddress",
                table: "AccommodationRooms");
        }
    }
}
