

using Microsoft.EntityFrameworkCore;
using MSHB.Reservation.Layers.L01_Entities.Models;

namespace MSHB.Reservation.Layers.L02_DataLayer

{
    public class ReservationDbContext : DbContext
    {
        public ReservationDbContext()
        {
        }
        public ReservationDbContext(DbContextOptions options)
: base(options)
        {
        }
        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
        public virtual DbSet<GroupAuth> GroupAuths { get; set; }
        public virtual DbSet<GroupAuthRole> GroupAuthRoles { get; set; }
        public virtual DbSet<AppLogItem> Logs { get; set; }
        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<AccommodationRoom> AccommodationRooms { get; set; }
        public virtual DbSet<AccommodationUserRoom> AccommodationUserRooms { get; set; }
        public virtual DbSet<AccommodationUserAttachment> AccommodationUserAttachments { get; set; }
        public virtual DbSet<UserConfiguration> UserConfigurations { get; set; }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(
            @"Data Source=103.215.222.35;Initial Catalog=ir_Reservation;Persist Security Info=True;User ID=ir_reservation;Password=Aa123456;");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Username).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Username).IsUnique();
                entity.Property(e => e.Password).IsRequired();
                entity.Property(e => e.SerialNumber).HasMaxLength(450);
                entity.HasIndex(b => b.Username);
                entity.HasIndex(b => b.Id);
                entity.HasIndex(b => b.GroupAuthId);
                entity.HasIndex(b => b.UserConfigurationId);

            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasIndex(b => b.Id);

            });

           
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.RoleId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.RoleId);
                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(d => d.RoleId);
                entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);

            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasOne(ut => ut.User)
                      .WithMany(u => u.UserTokens)
                      .HasForeignKey(ut => ut.UserId);
                entity.HasIndex(ut => ut.UserId);
                entity.Property(ut => ut.RefreshTokenIdHash).HasMaxLength(450).IsRequired();
                entity.Property(ut => ut.RefreshTokenIdHashSource).HasMaxLength(450);
            });

           
            modelBuilder.Entity<User>()
                         .HasOne(d => d.GroupAuth)
                         .WithMany(t => t.Users)
                         .HasForeignKey(d => d.GroupAuthId)
                         .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<User>()
                         .HasOne(d => d.City)
                         .WithMany(t => t.Users)
                         .HasForeignKey(d => d.CityId)
                         .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<City>()
                          .HasOne(d => d.Parent)
                          .WithMany(t => t.Children)
                          .HasForeignKey(t => t.ParentId)
                          .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<City>()
                           .HasIndex(d => d.CityName);


            modelBuilder.Entity<User>()
                  .HasMany(d => d.UserConfigurations).WithOne(d => d.User);

            modelBuilder.Entity<AccommodationRoom>()
                         .HasOne(d => d.City)
                         .WithMany(t => t.AccommodationRooms)
                         .HasForeignKey(d => d.CityId)
                         .OnDelete(DeleteBehavior.ClientSetNull);

                         modelBuilder.Entity<AccommodationUserRoom>()
                         .HasOne(d => d.City)
                         .WithMany(t => t.AccommodationUserRooms)
                         .HasForeignKey(d => d.CityId)
                         .OnDelete(DeleteBehavior.ClientSetNull);

                          modelBuilder.Entity<AccommodationUserRoom>()
                         .HasOne(d => d.User)
                         .WithMany(t => t.AccommodationUserRooms)
                         .HasForeignKey(d => d.UserId)
                         .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<AccommodationRoom>().HasIndex(c => c.RoomNumber);

            modelBuilder.HasSequence<long>("SystemCodeSequence").StartsAt(1000);

            modelBuilder.Entity<AccommodationUserRoom>().Property(e => e.SystemCode)
                .HasDefaultValueSql("NEXT VALUE FOR SystemCodeSequence");

            modelBuilder.Entity<GroupAuthRole>()
                     .HasKey(t => new { t.GroupAuthId, t.RoleId });

            modelBuilder.Entity<GroupAuthRole>()
                     .HasOne(pt => pt.GroupAuth)
                     .WithMany(p => p.GroupRoles)
                     .HasForeignKey(pt => pt.GroupAuthId);

            modelBuilder.Entity<GroupAuthRole>()
                    .HasOne(pt => pt.Role)
                    .WithMany()
                    .HasForeignKey(pt => pt.RoleId);


            modelBuilder.Entity<AppLogItem>()
                    .HasKey(t => new { t.Id });

           
            modelBuilder.Entity<City>()
                    .Property(c => c.CreationDate).HasDefaultValueSql("getdate()");

            modelBuilder.Entity<AccommodationUserRoom>(entity =>
            {               
                entity.HasIndex(e => e.NationalCode);
                entity.HasIndex(e => e.PhoneNumber);
                entity.HasIndex(e => e.SystemCode);

            });


        }

    }


}

