using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSHB.Reservation.Layers.L00_BaseModels.exceptions;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L03_Services.Contracts;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.Tree;
using MSHB.Reservation.Shared.Common.GuardToolkit;
using MSHB.Reservation.Layers.L00_BaseModels.Constants.Messages.Base;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using MSHB.Reservation.Layers.L02_DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using MSHB.Reservation.Layers.L00_BaseModels.Extensions;

namespace MSHB.Reservation.Layers.L03_Services.Impls
{
    public class CityService : ICityService
    {
        private readonly ReservationDbContext _context;

        public CityService(ReservationDbContext context)
        {
            _context = context;
            _context.CheckArgumentIsNull(nameof(_context));
        }

        public async Task<CityViewModel> GetAsync(User user, long Id)
        {
            try
            {
                var city = await _context.Citys.FindAsync(Id);
                if (city != null)
                {
                    return new CityViewModel()
                    {
                        Id = city.Id,
                        CityName = city.CityName,
                        Description = city.Description,
                        ParentId = city.ParentId,
                        DeactiveStartTime = city.DeactiveStartTime,
                        IsActivated = city.IsActivated
                    };
                }
                throw new ReservationGlobalException(CityServiceErrors.CityNotFoundError);
            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(CityServiceErrors.GetCityError, ex);
            }
        }

        public async Task<List<JsTreeNode>> GetCityByUserAsync(User user)
        {
            var cities = new List<City>();
            bool flagFilterTree = true;
            if (user.IsAdmin())
            {
                flagFilterTree = false;
                cities = await _context.Citys.ToListAsync();
            }
            else
            {
                user.CityId = (await _context.Users.FirstAsync(u => u.Id == user.Id)).CityId;
                cities = await _context.Citys.Where(x => x.Id == user.CityId).SelectMany(x => x.Children).ToListAsync();
            }
            var Citynodes = new List<JsTreeNode>();
            cities.ForEach(or =>
            {
                if (!flagFilterTree)
                {
                    if (or.ParentId == null)
                    {
                        JsTreeNode parentNode = new JsTreeNode();
                        parentNode.id = or.Id.ToString();
                        parentNode.text = or.CityName;
                        parentNode = FillChild(cities, parentNode, or.Id, null, false);
                        Citynodes.Add(parentNode);
                    }
                }
                else
                {
                    if (or.ParentId == user.CityId)
                    {
                        JsTreeNode parentNode = new JsTreeNode();
                        parentNode.id = or.Id.ToString();
                        parentNode.text = or.CityName;
                        parentNode = FillChild(or.Children.ToList(), parentNode, or.Id, null, false);
                        Citynodes.Add(parentNode);
                    }
                }

            });
            return Citynodes;
        }

        public async Task<List<JsTreeNode>> GetUserCityForUserAsync(User user, Guid userId)
        {
            var UserCityId = (await _context.Users.FindAsync(userId))?.CityId;
            bool flagFilterTree = true;

            var cities = new List<City>();
            if (user.IsAdmin())
            {
                flagFilterTree = false;
                cities = await _context.Citys.ToListAsync();
            }
            else
            {
                cities = await _context.Citys.Where(x => x.ParentId == UserCityId).SelectMany(x => x.Children).ToListAsync();
            }
            var citynodes = new List<JsTreeNode>();

            cities.ForEach(or =>
            {
                if (flagFilterTree)
                {
                    if (or.ParentId == null)
                    {
                        var flagHasIcon = false;
                        JsTreeNode parentNode = new JsTreeNode();
                        parentNode.id = or.Id.ToString();
                        parentNode.text = or.CityName;
                        if (UserCityId.HasValue && or.Id == UserCityId.Value)
                        {
                            parentNode.state.selected = true;

                        }
                        if (or.IsActivated.HasValue && !or.IsActivated.Value)
                        {
                            parentNode.icon = "glyphicon glyphicon-flash";
                            flagHasIcon = true;
                        }
                        parentNode = FillChild(or.Children.ToList(), parentNode, or.Id, UserCityId, flagHasIcon);
                        citynodes.Add(parentNode);
                    }
                }
                else
                {
                    if (or.ParentId == UserCityId)
                    {
                        var flagHasIcon = false;
                        JsTreeNode parentNode = new JsTreeNode();
                        parentNode.id = or.Id.ToString();
                        parentNode.text = or.CityName;
                        if (UserCityId.HasValue && or.Id == UserCityId.Value)
                        {
                            parentNode.state.selected = true;

                        }
                        if (or.IsActivated.HasValue && !or.IsActivated.Value)
                        {
                            parentNode.icon = "glyphicon glyphicon-flash";
                            flagHasIcon = true;
                        }
                        parentNode = FillChild(or.Children.ToList(), parentNode, or.Id, UserCityId, flagHasIcon);
                        citynodes.Add(parentNode);
                    }
                }
            });
            return citynodes;
        }

        private JsTreeNode FillChild(List<City> Citys, JsTreeNode parentNode, long Id, long? UserCityId, bool FlagHasIcon)
        {
            if (Citys.Count > 0)
            {
                Citys.ForEach(or =>
                {
                    if (or.ParentId == Id)
                    {
                        JsTreeNode parentNodeChild = new JsTreeNode();
                        parentNodeChild.id = or.Id.ToString();
                        parentNodeChild.text = or.CityName;
                        if (UserCityId.HasValue && or.Id == UserCityId.Value)
                        {
                            parentNodeChild.state.selected = true;

                        }
                        if ((or.IsActivated.HasValue && !or.IsActivated.Value) || FlagHasIcon)
                        {
                            parentNodeChild.icon = "glyphicon glyphicon-flash";
                            FlagHasIcon = true;
                        }
                        parentNode.children.Add(parentNodeChild);
                        FillChild(or.Children.ToList(), parentNodeChild, or.Id, UserCityId, FlagHasIcon);
                    }
                });
            }
            return parentNode;
        }
        public async Task<long> AddCityAsync(User user, AddcityFormModel cityForm)
        {
            try
            {
                City parent = null;
                if (cityForm.ParentId != null)
                    parent = _context.Citys.FirstOrDefault(c => c.ParentId == cityForm.ParentId);
                var isDuplicateCity = _context.Citys.Any(c => c.CityName == cityForm.CityName);
                if (!isDuplicateCity)
                {
                    var City = new City()
                    {
                        Description = cityForm.Description,
                        CityName = cityForm.CityName,
                        ParentId = cityForm.ParentId
                    };
                    await _context.Citys.AddAsync(City);
                    await _context.SaveChangesAsync();
                    return City.Id;
                }
                throw new ReservationGlobalException(CityServiceErrors.AddDuplicateCityError);

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(CityServiceErrors.AddCityError, ex);
            }


        }

        public async Task<bool> EditCityAsync(User user, EditcityFormModel cityForm)
        {
            try
            {
                City City = null;
                City = _context.Citys.FirstOrDefault(c => c.Id == cityForm.CityId);
                if (City != null)
                {
                    var isDuplicateCity = _context.Citys.Any(c => c.CityName == cityForm.CityName && c.Id != cityForm.CityId);
                    if (!isDuplicateCity)
                    {
                        City.Description = cityForm.Description;
                        City.CityName = cityForm.CityName;
                        City.ParentId = cityForm.ParentId;
                        City.LastUpdateDate = DateTime.Now;
                        _context.Citys.Update(City);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    throw new ReservationGlobalException(CityServiceErrors.EditDuplicateCityError);
                }
                throw new ReservationGlobalException(CityServiceErrors.EditCityNotExistError);

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(CityServiceErrors.EditCityError, ex);
            }
        }

        public async Task<bool> DeleteCityAsync(User user, List<long> CityIds)
        {
            try
            {

                foreach (var Cityid in CityIds)
                {
                    if (_context.Users.Any(c => c.CityId == Cityid))
                    {
                        throw new ReservationGlobalException(CityServiceErrors.UserInCityExistError);
                    }
                    var parent = _context.Citys.Include(p => p.Children)
                               .SingleOrDefault(p => p.Id == Cityid);

                    foreach (var child in parent.Children.ToList())
                    {
                        if (!CityIds.Contains(child.Id))
                        {
                            throw new ReservationGlobalException(CityServiceErrors.DeleteCityNotselectedError);
                        }
                        await DeleteCityByChildAsync(child, CityIds);
                        _context.Citys.Remove(child);
                    }
                    _context.Citys.Remove(parent);
                }
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(CityServiceErrors.DeleteCityError, ex);
            }
        }
        private async Task DeleteCityByChildAsync(City child, List<long> CityIds)
        {
            if (_context.Users.Any(c => c.CityId == child.Id))
            {
                throw new ReservationGlobalException(CityServiceErrors.UserInCityExistError);
            }
            var parentCity = _context.Citys.Include(p => p.Children)
                       .SingleOrDefault(p => p.Id == child.Id);

            foreach (var childCity in parentCity.Children.ToList())
            {
                if (!CityIds.Contains(childCity.Id))
                {
                    throw new ReservationGlobalException(CityServiceErrors.DeleteCityNotselectedError);
                }
                await DeleteCityByChildAsync(childCity, CityIds);
                _context.Citys.Remove(child);
            }
        }
        public async Task<bool> DeactivateCityAsync(User user, DeactivateCityFormModel cityForm)
        {
            try
            {
                City City = null;
                City = _context.Citys.FirstOrDefault(c => c.Id == cityForm.CityId);
                if (City != null)
                {
                    City.DeactiveStartTime = cityForm.DeactiveStartTime;
                    City.IsActivated = cityForm.IsActivated;
                    _context.Citys.Update(City);
                    await _context.SaveChangesAsync();
                    return true;
                }
                throw new ReservationGlobalException(CityServiceErrors.EditCityNotExistError);
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(CityServiceErrors.ChangeStatusError, ex);
            }
        }
    }
}
