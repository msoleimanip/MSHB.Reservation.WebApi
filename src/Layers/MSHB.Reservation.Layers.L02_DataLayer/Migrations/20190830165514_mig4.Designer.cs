﻿// <auto-generated />
using System;
using MSHB.Reservation.Layers.L02_DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MSHB.Reservation.Layers.L02_DataLayer.Migrations
{
    [DbContext(typeof(ReservationDbContext))]
    [Migration("20190830165514_mig4")]
    partial class mig4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("Relational:Sequence:.SystemCodeSequence", "'SystemCodeSequence', '', '1000', '1', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.Accommodation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Caption");

                    b.Property<long>("CityId");

                    b.Property<string>("Code");

                    b.Property<string>("District");

                    b.Property<Guid?>("FileId");

                    b.Property<bool>("IsActivated");

                    b.Property<string>("Number");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("Code");

                    b.ToTable("Accommodation_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationAttachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccommodationId");

                    b.Property<Guid?>("FileId");

                    b.Property<long?>("FileSize");

                    b.Property<string>("FileType")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.ToTable("AccommodationAttachment_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.AppLogItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedDateTime");

                    b.Property<int>("EventId");

                    b.Property<bool>("IsSoftDeleted");

                    b.Property<string>("LogLevel");

                    b.Property<string>("Logger");

                    b.Property<string>("Message");

                    b.Property<string>("StateJson");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Log_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.Bookination", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Mobile");

                    b.Property<string>("NationalityCode");

                    b.Property<Guid>("ReserveCode");

                    b.Property<DateTime>("StartDate");

                    b.Property<long>("UnitId");

                    b.HasKey("Id");

                    b.HasIndex("UnitId");

                    b.ToTable("Bookination_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.BookinationEntourage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age");

                    b.Property<long>("BookinationId");

                    b.Property<int>("GenderType");

                    b.Property<string>("Name");

                    b.Property<string>("NationalityCode");

                    b.Property<string>("Relative");

                    b.HasKey("Id");

                    b.HasIndex("BookinationId");

                    b.ToTable("BookinationEntourage_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ProvinceId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("City_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.FileAddress", b =>
                {
                    b.Property<Guid>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("FilePath");

                    b.Property<long?>("FileSize");

                    b.Property<string>("FileType")
                        .HasMaxLength(20);

                    b.Property<Guid>("UserId");

                    b.HasKey("FileId");

                    b.HasIndex("UserId");

                    b.ToTable("FileAddress_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.GroupAuth", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("GroupAuth_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.GroupAuthRole", b =>
                {
                    b.Property<long?>("GroupAuthId");

                    b.Property<long>("RoleId");

                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("GroupAuthId", "RoleId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("GroupAuthRole_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.Province", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Province_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.ReportStructure", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Configuration");

                    b.Property<DateTime>("CreationDate");

                    b.Property<DateTime>("LastUpdatedDateTime");

                    b.Property<string>("ProtoType");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportStructure_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Role_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.Unit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccommodationId");

                    b.Property<int>("AccommodationType");

                    b.Property<int>("DoubleBedCount");

                    b.Property<bool>("IsActive");

                    b.Property<int>("MaximumCount");

                    b.Property<int>("MinimumCount");

                    b.Property<int>("RoomCount");

                    b.Property<int>("SingleBedCount");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationId");

                    b.ToTable("Unit_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CityId");

                    b.Property<DateTime?>("CreationDate");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("FirstName")
                        .HasMaxLength(500);

                    b.Property<long?>("GroupAuthId");

                    b.Property<bool>("IsActive");

                    b.Property<int>("IsPresident");

                    b.Property<DateTime?>("LastLockoutDate");

                    b.Property<DateTimeOffset?>("LastLoggedIn");

                    b.Property<string>("LastName")
                        .HasMaxLength(500);

                    b.Property<DateTime?>("LastPasswordChangedDate");

                    b.Property<DateTime?>("LastVisit");

                    b.Property<string>("Location")
                        .HasMaxLength(100);

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20);

                    b.Property<string>("SerialNumber")
                        .HasMaxLength(450);

                    b.Property<long?>("UserConfigurationId");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("GroupAuthId");

                    b.HasIndex("Id");

                    b.HasIndex("UserConfigurationId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("User_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.UserConfiguration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Configuration");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserConfiguration_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.UserRole", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<long>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.UserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("AccessTokenExpiresDateTime");

                    b.Property<string>("AccessTokenHash");

                    b.Property<DateTimeOffset>("RefreshTokenExpiresDateTime");

                    b.Property<string>("RefreshTokenIdHash")
                        .IsRequired()
                        .HasMaxLength(450);

                    b.Property<string>("RefreshTokenIdHashSource")
                        .HasMaxLength(450);

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserToken_T");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.Accommodation", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.City", "City")
                        .WithMany("Accommodations")
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationAttachment", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.Accommodation", "Accommodation")
                        .WithMany("AccommodationAttachments")
                        .HasForeignKey("AccommodationId");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.Bookination", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.Unit", "Unit")
                        .WithMany("Bookinations")
                        .HasForeignKey("UnitId");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.BookinationEntourage", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.Bookination", "Bookination")
                        .WithMany("BookinationEntourages")
                        .HasForeignKey("BookinationId");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.City", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.FileAddress", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.GroupAuthRole", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.GroupAuth", "GroupAuth")
                        .WithMany("GroupRoles")
                        .HasForeignKey("GroupAuthId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.Unit", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.Accommodation", "Accommodation")
                        .WithMany("Units")
                        .HasForeignKey("AccommodationId");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.User", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.GroupAuth", "GroupAuth")
                        .WithMany("Users")
                        .HasForeignKey("GroupAuthId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.UserConfiguration", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.User", "User")
                        .WithMany("UserConfigurations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.UserRole", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.UserToken", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.User", "User")
                        .WithMany("UserTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
