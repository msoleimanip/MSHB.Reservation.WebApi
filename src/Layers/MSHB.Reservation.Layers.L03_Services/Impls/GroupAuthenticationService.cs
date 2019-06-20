using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base;
using MSHB.Reservation.Layers.L02_DataLayer;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{

    public class GroupAuthenticationService : IGroupAuthenticationService
    {        
        private readonly ReservationDbContext _context;

        public GroupAuthenticationService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }
        public async Task<List<GroupAuthenticationViewModel>> GetGroupAuthenticationAsync()
        {
            var groupAuthentications =await  _context.GroupAuths.ToListAsync();
            return groupAuthentications.Select(x => new GroupAuthenticationViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();
        }

        public async Task<long> AddGroupAsync(User user, AddGroupFormModel groupForm)
        {
            try
            {                       
                var sameGroup = await _context.GroupAuths.Where(c => c.Name == groupForm.Name).ToListAsync();
                if (sameGroup.Count > 0)
                {
                    throw new ReservationGlobalException(GroupServiceErrors.SameGroupExistError);
                }
                if (_context.Roles.Any(c => !groupForm.RoleIds.Contains(c.Id)))
                {
                    throw new ReservationGlobalException(GroupServiceErrors.RoleExistError);

                }
                var group = new GroupAuth()
                {
                    Name = groupForm.Name,
                    Description = groupForm.Description
                };
                _context.GroupAuths.Add(group);

                foreach (var gfRole in groupForm.RoleIds)
                {
                    var groupAuthRole = new GroupAuthRole()
                    {
                        RoleId = gfRole,
                        GroupAuthId = group.Id
                    };
                    _context.GroupAuthRoles.Add(groupAuthRole);
                }
                await _context.SaveChangesAsync();
                return group.Id;


            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(GroupServiceErrors.AddGroupError, ex);
            }
        }

        public async Task<bool> DeleteGroupAsync(User user, List<long> groupIds)
        {
            try
            {
                
                    if (_context.GroupAuths.Any(c => !groupIds.Contains(c.Id)))
                    {
                        throw new ReservationGlobalException(GroupServiceErrors.DeleteGroupNotselectedError);

                    }
                    if (_context.GroupAuths.Where(c => groupIds.Contains(c.Id)).Include(c => c.Users).Any())
                    {
                        throw new ReservationGlobalException(GroupServiceErrors.UserInGroupExistError);

                    }
                    _context.GroupAuths.RemoveRange(_context.GroupAuths.Where(c => groupIds.Contains(c.Id)));
                    await _context.SaveChangesAsync();
                    return true;
                
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(GroupServiceErrors.DeleteGroupError, ex);
            }
        }

        public async Task<bool> EditGroupAsync(User user, EditGroupFormModel groupForm)
        {
            try
            {   
                    GroupAuth group = null;
                    if (_context.Roles.Any(c => !groupForm.RoleIds.Contains(c.Id)))
                    {
                        throw new ReservationGlobalException(GroupServiceErrors.RoleExistError);

                    }
                    group = _context.GroupAuths.Include(c => c.Users).Include(c => c.GroupRoles).FirstOrDefault(c => c.Id == groupForm.GroupId);
                    if (group != null)
                    {
                        var isDuplicategroup = _context.GroupAuths.Any(c => c.Name == groupForm.Name && c.Id != groupForm.GroupId);
                        if (!isDuplicategroup)
                        {
                            var oldRoleIds = group.GroupRoles.Select(c => c.RoleId).ToList();
                            IEnumerable<long> differencesFirst = groupForm.RoleIds.Except(oldRoleIds).Union(oldRoleIds.Except(groupForm.RoleIds));
                            IEnumerable<long> differencesSecond = groupForm.RoleIds.Union(oldRoleIds).Except(groupForm.RoleIds.Intersect(oldRoleIds));
                            if (differencesFirst.Count() > 0 || differencesSecond.Count() > 0)
                            {
                                foreach (var gpUser in group.Users.ToList())
                                {
                                _context.UserRoles.RemoveRange(_context.UserRoles.Where(c => c.UserId == gpUser.Id).ToList());
                                }
                                foreach (var gpUser in group.Users.ToList())
                                {
                                    var newUserRoles = groupForm.RoleIds.Select(roleId =>
                                                    new UserRole
                                                    {
                                                        UserId = gpUser.Id,
                                                        RoleId = roleId
                                                    }).ToList();
                                _context.UserRoles.AddRange(newUserRoles);
                                }
                            }
                            group.Name = groupForm.Name;
                            group.Description = groupForm.Description;
                            await _context.SaveChangesAsync();
                            return true;
                        }
                        throw new ReservationGlobalException(GroupServiceErrors.EditDuplicateGroupError);
                    }
                    throw new ReservationGlobalException(GroupServiceErrors.EditGroupNotExistError);

                
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(GroupServiceErrors.EditGroupError, ex);
            }
        }

        public async Task<List<RoleViewModel>> GetGroupRoleAsync(User user, long Id)
        {
            try
            {
                var roles = await _context.GroupAuthRoles.Where(c => c.GroupAuthId == Id).Include(c => c.Role).Select(c => c.Role).ToListAsync();

                return roles.Select(x => new RoleViewModel
                {
                    RoleId = x.Id,
                    Name = x.Name,
                    Title = x.Title
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(GroupServiceErrors.GetRolesError, ex);
            }
        }

        public async Task<GroupAuth> GetGroupByIdAsync(User user, long groupAuthId)
        {
            return  await _context.GroupAuths.FindAsync(groupAuthId);
        }
    }
}
