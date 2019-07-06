using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationRooms_City_T_CityId",
                table: "AccommodationRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationUserAttachments_AccommodationUserRooms_AccommodationUserRoomId",
                table: "AccommodationUserAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationUserRooms_AccommodationRooms_AccommodationRoomId",
                table: "AccommodationUserRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationUserRooms_City_T_CityId",
                table: "AccommodationUserRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationUserRooms_User_T_UserId",
                table: "AccommodationUserRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_CityAttachments_City_T_CityId",
                table: "CityAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_FileAddresses_User_T_UserId",
                table: "FileAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileAddresses",
                table: "FileAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityAttachments",
                table: "CityAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationUserRooms",
                table: "AccommodationUserRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationUserAttachments",
                table: "AccommodationUserAttachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationRooms",
                table: "AccommodationRooms");

            migrationBuilder.RenameTable(
                name: "FileAddresses",
                newName: "FileAddress_T");

            migrationBuilder.RenameTable(
                name: "CityAttachments",
                newName: "CityAttachment_T");

            migrationBuilder.RenameTable(
                name: "AccommodationUserRooms",
                newName: "AccommodationUserRoom_T");

            migrationBuilder.RenameTable(
                name: "AccommodationUserAttachments",
                newName: "AccommodationUserAttachment_T");

            migrationBuilder.RenameTable(
                name: "AccommodationRooms",
                newName: "AccommodationRoom_T");

            migrationBuilder.RenameIndex(
                name: "IX_FileAddresses_UserId",
                table: "FileAddress_T",
                newName: "IX_FileAddress_T_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CityAttachments_CityId",
                table: "CityAttachment_T",
                newName: "IX_CityAttachment_T_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRooms_UserId",
                table: "AccommodationUserRoom_T",
                newName: "IX_AccommodationUserRoom_T_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRooms_SystemCode",
                table: "AccommodationUserRoom_T",
                newName: "IX_AccommodationUserRoom_T_SystemCode");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRooms_PhoneNumber",
                table: "AccommodationUserRoom_T",
                newName: "IX_AccommodationUserRoom_T_PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRooms_NationalCode",
                table: "AccommodationUserRoom_T",
                newName: "IX_AccommodationUserRoom_T_NationalCode");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRooms_CityId",
                table: "AccommodationUserRoom_T",
                newName: "IX_AccommodationUserRoom_T_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRooms_AccommodationRoomId",
                table: "AccommodationUserRoom_T",
                newName: "IX_AccommodationUserRoom_T_AccommodationRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserAttachments_AccommodationUserRoomId",
                table: "AccommodationUserAttachment_T",
                newName: "IX_AccommodationUserAttachment_T_AccommodationUserRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationRooms_RoomNumber",
                table: "AccommodationRoom_T",
                newName: "IX_AccommodationRoom_T_RoomNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationRooms_CityId",
                table: "AccommodationRoom_T",
                newName: "IX_AccommodationRoom_T_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileAddress_T",
                table: "FileAddress_T",
                column: "FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityAttachment_T",
                table: "CityAttachment_T",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationUserRoom_T",
                table: "AccommodationUserRoom_T",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationUserAttachment_T",
                table: "AccommodationUserAttachment_T",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationRoom_T",
                table: "AccommodationRoom_T",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ReportStructure_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReportId = table.Column<string>(nullable: false),
                    Configuration = table.Column<string>(nullable: true),
                    ProtoType = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStructure_T", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRoom_T_City_T_CityId",
                table: "AccommodationRoom_T",
                column: "CityId",
                principalTable: "City_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationUserAttachment_T_AccommodationUserRoom_T_AccommodationUserRoomId",
                table: "AccommodationUserAttachment_T",
                column: "AccommodationUserRoomId",
                principalTable: "AccommodationUserRoom_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationUserRoom_T_AccommodationRoom_T_AccommodationRoomId",
                table: "AccommodationUserRoom_T",
                column: "AccommodationRoomId",
                principalTable: "AccommodationRoom_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationUserRoom_T_City_T_CityId",
                table: "AccommodationUserRoom_T",
                column: "CityId",
                principalTable: "City_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationUserRoom_T_User_T_UserId",
                table: "AccommodationUserRoom_T",
                column: "UserId",
                principalTable: "User_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CityAttachment_T_City_T_CityId",
                table: "CityAttachment_T",
                column: "CityId",
                principalTable: "City_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileAddress_T_User_T_UserId",
                table: "FileAddress_T",
                column: "UserId",
                principalTable: "User_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationRoom_T_City_T_CityId",
                table: "AccommodationRoom_T");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationUserAttachment_T_AccommodationUserRoom_T_AccommodationUserRoomId",
                table: "AccommodationUserAttachment_T");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationUserRoom_T_AccommodationRoom_T_AccommodationRoomId",
                table: "AccommodationUserRoom_T");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationUserRoom_T_City_T_CityId",
                table: "AccommodationUserRoom_T");

            migrationBuilder.DropForeignKey(
                name: "FK_AccommodationUserRoom_T_User_T_UserId",
                table: "AccommodationUserRoom_T");

            migrationBuilder.DropForeignKey(
                name: "FK_CityAttachment_T_City_T_CityId",
                table: "CityAttachment_T");

            migrationBuilder.DropForeignKey(
                name: "FK_FileAddress_T_User_T_UserId",
                table: "FileAddress_T");

            migrationBuilder.DropTable(
                name: "ReportStructure_T");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileAddress_T",
                table: "FileAddress_T");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityAttachment_T",
                table: "CityAttachment_T");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationUserRoom_T",
                table: "AccommodationUserRoom_T");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationUserAttachment_T",
                table: "AccommodationUserAttachment_T");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccommodationRoom_T",
                table: "AccommodationRoom_T");

            migrationBuilder.RenameTable(
                name: "FileAddress_T",
                newName: "FileAddresses");

            migrationBuilder.RenameTable(
                name: "CityAttachment_T",
                newName: "CityAttachments");

            migrationBuilder.RenameTable(
                name: "AccommodationUserRoom_T",
                newName: "AccommodationUserRooms");

            migrationBuilder.RenameTable(
                name: "AccommodationUserAttachment_T",
                newName: "AccommodationUserAttachments");

            migrationBuilder.RenameTable(
                name: "AccommodationRoom_T",
                newName: "AccommodationRooms");

            migrationBuilder.RenameIndex(
                name: "IX_FileAddress_T_UserId",
                table: "FileAddresses",
                newName: "IX_FileAddresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CityAttachment_T_CityId",
                table: "CityAttachments",
                newName: "IX_CityAttachments_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRoom_T_UserId",
                table: "AccommodationUserRooms",
                newName: "IX_AccommodationUserRooms_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRoom_T_SystemCode",
                table: "AccommodationUserRooms",
                newName: "IX_AccommodationUserRooms_SystemCode");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRoom_T_PhoneNumber",
                table: "AccommodationUserRooms",
                newName: "IX_AccommodationUserRooms_PhoneNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRoom_T_NationalCode",
                table: "AccommodationUserRooms",
                newName: "IX_AccommodationUserRooms_NationalCode");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRoom_T_CityId",
                table: "AccommodationUserRooms",
                newName: "IX_AccommodationUserRooms_CityId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserRoom_T_AccommodationRoomId",
                table: "AccommodationUserRooms",
                newName: "IX_AccommodationUserRooms_AccommodationRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationUserAttachment_T_AccommodationUserRoomId",
                table: "AccommodationUserAttachments",
                newName: "IX_AccommodationUserAttachments_AccommodationUserRoomId");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationRoom_T_RoomNumber",
                table: "AccommodationRooms",
                newName: "IX_AccommodationRooms_RoomNumber");

            migrationBuilder.RenameIndex(
                name: "IX_AccommodationRoom_T_CityId",
                table: "AccommodationRooms",
                newName: "IX_AccommodationRooms_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileAddresses",
                table: "FileAddresses",
                column: "FileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityAttachments",
                table: "CityAttachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationUserRooms",
                table: "AccommodationUserRooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationUserAttachments",
                table: "AccommodationUserAttachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccommodationRooms",
                table: "AccommodationRooms",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationRooms_City_T_CityId",
                table: "AccommodationRooms",
                column: "CityId",
                principalTable: "City_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationUserAttachments_AccommodationUserRooms_AccommodationUserRoomId",
                table: "AccommodationUserAttachments",
                column: "AccommodationUserRoomId",
                principalTable: "AccommodationUserRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationUserRooms_AccommodationRooms_AccommodationRoomId",
                table: "AccommodationUserRooms",
                column: "AccommodationRoomId",
                principalTable: "AccommodationRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationUserRooms_City_T_CityId",
                table: "AccommodationUserRooms",
                column: "CityId",
                principalTable: "City_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccommodationUserRooms_User_T_UserId",
                table: "AccommodationUserRooms",
                column: "UserId",
                principalTable: "User_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CityAttachments_City_T_CityId",
                table: "CityAttachments",
                column: "CityId",
                principalTable: "City_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileAddresses_User_T_UserId",
                table: "FileAddresses",
                column: "UserId",
                principalTable: "User_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
