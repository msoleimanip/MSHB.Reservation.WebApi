using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Transactions;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;

namespace MSHB.Reservation.Layers.L03_Services.Initialization
{
    public interface IDbInitializerService
    {
        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// Will create the database if it does not already exist.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Adds some default values to the Db
        /// </summary>
        void SeedData();
    }

    public class DbInitializerService : IDbInitializerService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ISecurityService _securityService;

        public DbInitializerService(
            IServiceScopeFactory scopeFactory,
            ISecurityService securityService)
        {
            _scopeFactory = scopeFactory;
            _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ReservationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {

            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ReservationDbContext>())
                {


                    // Add default roles
                    var adminRole = CustomRoles.GetInitialRoles();

                    if (!context.Roles.Any())
                    {
                        context.AddRange(adminRole);

                        context.SaveChanges();
                    }

                    // Add Admin user
                    if (!context.Users.Any())
                    {
                        var groupAuth = new GroupAuth()
                        {
                            Name = "Administrator",
                            Description = "Administrator"
                        };

                        var adminUser = new User
                        {
                            Username = "MSHB",
                            FirstName = "Mohammad",
                            LastName = "Soleimani",
                            IsActive = true,
                            IsPresident = 1,
                            LastLoggedIn = null,
                            Password = _securityService.GetSha256Hash("1234"),
                            SerialNumber = Guid.NewGuid().ToString("N"),
                            GroupAuth = groupAuth
                        };

                        context.GroupAuths.Add(groupAuth);
                        context.Users.Add(adminUser);

                        foreach (var role in context.Roles.ToList())
                        {
                            context.Add(new UserRole { Role = role, User = adminUser });
                            if (groupAuth != null)
                            {
                                context.GroupAuthRoles.Add(new GroupAuthRole { GroupAuthId = groupAuth.Id, RoleId = role.Id });
                            }
                        }

                        context.SaveChanges();

                    }

                }
            }
        }
    }
}