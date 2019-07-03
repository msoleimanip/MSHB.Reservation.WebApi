using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Transactions;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L02_DataLayer;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L01_Entities.Enums;

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
        private readonly ReservationDbContext _context;

        public DbInitializerService(
            IServiceScopeFactory scopeFactory,
            ISecurityService securityService, ReservationDbContext context)
        {
            _scopeFactory = scopeFactory;
            _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }

        public void Initialize()
        {
            
                    _context.Database.Migrate();       
        }

        public void SeedData()
        {

           
                


                    // Add default roles
                    var adminRole = CustomRoles.GetInitialRoles();

                    if (!_context.Roles.Any())
                    {
                    _context.AddRange(adminRole);

                    _context.SaveChanges();
                    }

                    // Add Admin user
                    if (!_context.Users.Any())
                    {
                        var groupAuth = new GroupAuth()
                        {
                            Name = "Administrator",
                            Description = "Administrator"
                        };

                        var adminUser = new User
                        {
                            Username = "admin",
                            FirstName = "مدیر",
                            LastName = "سیستم",
                            IsActive = true,
                            IsPresident = PresidentType.Admin,
                            LastLoggedIn = null,
                            Password = _securityService.GetSha256Hash("1234"),
                            SerialNumber = Guid.NewGuid().ToString("N"),
                            GroupAuth = groupAuth
                        };

                    _context.GroupAuths.Add(groupAuth);
                    _context.Users.Add(adminUser);

                        foreach (var role in _context.Roles.ToList())
                        {
                        _context.Add(new UserRole { Role = role, User = adminUser });
                            if (groupAuth != null)
                            {
                            _context.GroupAuthRoles.Add(new GroupAuthRole { GroupAuthId = groupAuth.Id, RoleId = role.Id });
                            }
                        }

                    _context.SaveChanges();

                   

                }
           
        }
    }
}