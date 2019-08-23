using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookinationEntourage_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BookinationId = table.Column<long>(nullable: false),
                    GenderType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NationalityCode = table.Column<string>(nullable: true),
                    Relative = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookinationEntourage_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookinationEntourage_T_Bookination_T_BookinationId",
                        column: x => x.BookinationId,
                        principalTable: "Bookination_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookinationEntourage_T_BookinationId",
                table: "BookinationEntourage_T",
                column: "BookinationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookinationEntourage_T");
        }
    }
}
