using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileAddresses",
                columns: table => new
                {
                    FileId = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    FileType = table.Column<string>(maxLength: 20, nullable: true),
                    FileSize = table.Column<long>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAddresses", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileAddresses_User_T_UserId",
                        column: x => x.UserId,
                        principalTable: "User_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileAddresses_UserId",
                table: "FileAddresses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileAddresses");
        }
    }
}
