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
    [Migration("20190624101423_mig4")]
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

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationRoom", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Bed");

                    b.Property<int?>("BedRoom");

                    b.Property<int?>("Capacity");

                    b.Property<long?>("CityId");

                    b.Property<string>("Description");

                    b.Property<bool?>("IsActivated");

                    b.Property<int>("Rank");

                    b.Property<string>("RoomNumber")
                        .HasMaxLength(20);

                    b.Property<long>("RoomPrice");

                    b.Property<int>("RoomType");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("RoomNumber");

                    b.ToTable("AccommodationRooms");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationUserAttachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccommodationUserRoomId");

                    b.Property<int?>("Age");

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("GenderType");

                    b.Property<string>("Name");

                    b.Property<string>("NationalCode")
                        .HasMaxLength(20);

                    b.Property<string>("Relative");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationUserRoomId");

                    b.ToTable("AccommodationUserAttachments");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationUserRoom", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AccommodationRoomId");

                    b.Property<long>("CityId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<long>("Description");

                    b.Property<DateTime?>("EndTime");

                    b.Property<DateTime?>("EntranceTime");

                    b.Property<int>("GenderType");

                    b.Property<int>("GuestCounts");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<string>("NationalCode")
                        .HasMaxLength(12);

                    b.Property<int>("PaymentType");

                    b.Property<string>("PersonalCode");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(15);

                    b.Property<long>("PriceAccommodation");

                    b.Property<long>("SystemCode")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("NEXT VALUE FOR SystemCodeSequence");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("AccommodationRoomId");

                    b.HasIndex("CityId");

                    b.HasIndex("NationalCode");

                    b.HasIndex("PhoneNumber");

                    b.HasIndex("SystemCode");

                    b.HasIndex("UserId");

                    b.ToTable("AccommodationUserRooms");
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

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CityName")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreationDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("DeactiveStartTime");

                    b.Property<string>("Description");

                    b.Property<bool?>("IsActivated");

                    b.Property<DateTime?>("LastUpdateDate");

                    b.Property<double?>("Latitude");

                    b.Property<double?>("Longitude");

                    b.Property<long?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("CityName");

                    b.HasIndex("ParentId");

                    b.ToTable("City_T");
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

                    b.Property<string>("SajadUserName")
                        .HasMaxLength(200);

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

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationRoom", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.City", "City")
                        .WithMany("AccommodationRooms")
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationUserAttachment", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationUserRoom", "AccommodationUserRoom")
                        .WithMany("AccommodationUserAttachments")
                        .HasForeignKey("AccommodationUserRoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationUserRoom", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.AccommodationRoom", "AccommodationRoom")
                        .WithMany("AccommodationUserRooms")
                        .HasForeignKey("AccommodationRoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.City", "City")
                        .WithMany("AccommodationUserRooms")
                        .HasForeignKey("CityId");

                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.User", "User")
                        .WithMany("AccommodationUserRooms")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.City", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.City", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId");
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

            modelBuilder.Entity("MSHB.Reservation.Layers.L01_Entities.Models.User", b =>
                {
                    b.HasOne("MSHB.Reservation.Layers.L01_Entities.Models.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

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
