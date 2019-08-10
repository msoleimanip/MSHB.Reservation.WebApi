using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SystemCodeSequence",
                startValue: 1000L);

            migrationBuilder.CreateTable(
                name: "GroupAuth_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAuth_T", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log_T",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    LogLevel = table.Column<string>(nullable: true),
                    Logger = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    StateJson = table.Column<string>(nullable: true),
                    IsSoftDeleted = table.Column<bool>(nullable: false),
                    CreatedDateTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log_T", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province_T", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportStructure_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReportId = table.Column<string>(maxLength: 40, nullable: false),
                    Configuration = table.Column<string>(nullable: true),
                    ProtoType = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportStructure_T", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 450, nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_T", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_T_Province_T_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupAuthRole_T",
                columns: table => new
                {
                    GroupAuthId = table.Column<long>(nullable: false),
                    RoleId = table.Column<long>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupAuthRole_T", x => new { x.GroupAuthId, x.RoleId });
                    table.UniqueConstraint("AK_GroupAuthRole_T_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupAuthRole_T_GroupAuth_T_GroupAuthId",
                        column: x => x.GroupAuthId,
                        principalTable: "GroupAuth_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupAuthRole_T_Role_T_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accommodation_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caption = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    IsActivated = table.Column<bool>(nullable: false),
                    District = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    FileId = table.Column<Guid>(nullable: true),
                    CityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodation_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodation_T_City_T_CityId",
                        column: x => x.CityId,
                        principalTable: "City_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User_T",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Username = table.Column<string>(maxLength: 450, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 500, nullable: true),
                    LastName = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Location = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    LastLockoutDate = table.Column<DateTime>(nullable: true),
                    LastPasswordChangedDate = table.Column<DateTime>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: true),
                    LastVisit = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    LastLoggedIn = table.Column<DateTimeOffset>(nullable: true),
                    SerialNumber = table.Column<string>(maxLength: 450, nullable: true),
                    IsPresident = table.Column<int>(nullable: false),
                    GroupAuthId = table.Column<long>(nullable: true),
                    CityId = table.Column<long>(nullable: true),
                    UserConfigurationId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_T_City_T_CityId",
                        column: x => x.CityId,
                        principalTable: "City_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_T_GroupAuth_T_GroupAuthId",
                        column: x => x.GroupAuthId,
                        principalTable: "GroupAuth_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccommodationAttachment_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccommodationId = table.Column<long>(nullable: false),
                    FileType = table.Column<string>(maxLength: 20, nullable: true),
                    FileSize = table.Column<long>(nullable: true),
                    FileId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccommodationAttachment_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccommodationAttachment_T_Accommodation_T_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodation_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Unit_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccommodationType = table.Column<int>(nullable: false),
                    RoomCount = table.Column<int>(nullable: false),
                    SingleBedCount = table.Column<int>(nullable: false),
                    DoubleBedCount = table.Column<int>(nullable: false),
                    MinimumCount = table.Column<int>(nullable: false),
                    MaximumCount = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    AccommodationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Unit_T_Accommodation_T_AccommodationId",
                        column: x => x.AccommodationId,
                        principalTable: "Accommodation_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileAddress_T",
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
                    table.PrimaryKey("PK_FileAddress_T", x => x.FileId);
                    table.ForeignKey(
                        name: "FK_FileAddress_T_User_T_UserId",
                        column: x => x.UserId,
                        principalTable: "User_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserConfiguration_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    Configuration = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConfiguration_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserConfiguration_T_User_T_UserId",
                        column: x => x.UserId,
                        principalTable: "User_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole_T",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole_T", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_T_Role_T_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_T_User_T_UserId",
                        column: x => x.UserId,
                        principalTable: "User_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToken_T",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccessTokenHash = table.Column<string>(nullable: true),
                    AccessTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    RefreshTokenIdHash = table.Column<string>(maxLength: 450, nullable: false),
                    RefreshTokenIdHashSource = table.Column<string>(maxLength: 450, nullable: true),
                    RefreshTokenExpiresDateTime = table.Column<DateTimeOffset>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_T_User_T_UserId",
                        column: x => x.UserId,
                        principalTable: "User_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_T_CityId",
                table: "Accommodation_T",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodation_T_Code",
                table: "Accommodation_T",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AccommodationAttachment_T_AccommodationId",
                table: "AccommodationAttachment_T",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_City_T_ProvinceId",
                table: "City_T",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_FileAddress_T_UserId",
                table: "FileAddress_T",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupAuthRole_T_RoleId",
                table: "GroupAuthRole_T",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportStructure_T_ReportId",
                table: "ReportStructure_T",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_T_Id",
                table: "Role_T",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Role_T_Name",
                table: "Role_T",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Unit_T_AccommodationId",
                table: "Unit_T",
                column: "AccommodationId");

            migrationBuilder.CreateIndex(
                name: "IX_User_T_CityId",
                table: "User_T",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_User_T_GroupAuthId",
                table: "User_T",
                column: "GroupAuthId");

            migrationBuilder.CreateIndex(
                name: "IX_User_T_Id",
                table: "User_T",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_T_UserConfigurationId",
                table: "User_T",
                column: "UserConfigurationId");

            migrationBuilder.CreateIndex(
                name: "IX_User_T_Username",
                table: "User_T",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserConfiguration_T_UserId",
                table: "UserConfiguration_T",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_T_RoleId",
                table: "UserRole_T",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_T_UserId",
                table: "UserRole_T",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_T_UserId",
                table: "UserToken_T",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccommodationAttachment_T");

            migrationBuilder.DropTable(
                name: "FileAddress_T");

            migrationBuilder.DropTable(
                name: "GroupAuthRole_T");

            migrationBuilder.DropTable(
                name: "Log_T");

            migrationBuilder.DropTable(
                name: "ReportStructure_T");

            migrationBuilder.DropTable(
                name: "Unit_T");

            migrationBuilder.DropTable(
                name: "UserConfiguration_T");

            migrationBuilder.DropTable(
                name: "UserRole_T");

            migrationBuilder.DropTable(
                name: "UserToken_T");

            migrationBuilder.DropTable(
                name: "Accommodation_T");

            migrationBuilder.DropTable(
                name: "Role_T");

            migrationBuilder.DropTable(
                name: "User_T");

            migrationBuilder.DropTable(
                name: "City_T");

            migrationBuilder.DropTable(
                name: "GroupAuth_T");

            migrationBuilder.DropTable(
                name: "Province_T");

            migrationBuilder.DropSequence(
                name: "SystemCodeSequence");
        }
    }
}
