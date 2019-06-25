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
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MSHB.Reservation.Layers.L00_BaseModels.Settings;
using System.IO;
using System.Drawing;

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
                var city = await _context.Citys.Include(c=>c.CityAttachments).FirstOrDefaultAsync(c=>c.Id==Id);
                if (city != null)
                {
                    var cityViewModel= new CityViewModel()
                    {
                        Id = city.Id,
                        CityName = city.CityName,
                        Description = city.Description,
                        ParentId = city.ParentId,
                        DeactiveStartTime = city.DeactiveStartTime,
                        IsActivated = city.IsActivated,
                        Latitude = city.Latitude,
                        Longitude = city.Longitude,
                        ImageAddress = !string.IsNullOrEmpty(city.ImageAddress)?new System.Uri(city.ImageAddress).AbsoluteUri:"",
                    };
                    city.CityAttachments.ToList().ForEach(c =>
                    {
                        var cAtt = new CityAttachmentViewModel()
                        {
                            FileSize = c.FileSize,
                            FileType = c.FileType,
                            UrlFile = !string.IsNullOrEmpty(c.FilePath) ? new System.Uri(c.FilePath).AbsoluteUri : "",
                            CityId = c.CityId,
                            Id = c.Id,
                        };
                        cityViewModel.CityDetailAttachments.Add(cAtt);

                    });
                    return cityViewModel;


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
                        ParentId = cityForm.ParentId,
                        

                    };
                    FileAddress fileAddress;
                    if (cityForm.ImageId != null)
                    {
                        fileAddress = await _context.FileAddresses.FindAsync(cityForm.ImageId);
                        if (fileAddress == null)
                        {
                            throw new ReservationGlobalException(CityServiceErrors.NotExistFileAddresstError);
                        }
                        try
                        {
                            if (!File.Exists(fileAddress.FilePath))
                            {
                                throw new ReservationGlobalException(CityServiceErrors.FileNotFoundError);
                               
                            }
                            Image image = Image.FromFile(fileAddress.FilePath);
                            Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                            var newPath = Path.ChangeExtension(fileAddress.FilePath, "thumb");
                            thumb.Save(newPath);
                            City.ImageAddress = newPath;
                        }
                        catch (Exception ex)
                        {

                            throw new ReservationGlobalException(CityServiceErrors.ChangeToThumbnailError, ex);
                        }

                    }
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
                        FileAddress fileAddress;
                        if (cityForm.ImageId != null)
                        {
                            fileAddress = await _context.FileAddresses.FindAsync(cityForm.ImageId);
                            if (fileAddress == null)
                            {
                                throw new ReservationGlobalException(CityServiceErrors.NotExistFileAddresstError);
                            }
                            try
                            {
                                if (!File.Exists(fileAddress.FilePath))
                                {
                                    throw new ReservationGlobalException(CityServiceErrors.FileNotFoundError);

                                }
                                Image image = Image.FromFile(fileAddress.FilePath);
                                Image thumb = image.GetThumbnailImage(120, 120, () => false, IntPtr.Zero);
                                var newPath = Path.ChangeExtension(fileAddress.FilePath, "thumb");
                                thumb.Save(newPath);
                                City.ImageAddress = newPath;
                            }
                            catch (Exception ex)
                            {

                                throw new ReservationGlobalException(CityServiceErrors.ChangeToThumbnailError, ex);
                            }

                        }
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

        public async Task<bool> SetCityLocationAsync(User user, CityLocationFormModel cityLocationForm)
        {
            try
            {
                City City = null;
                City = _context.Citys.FirstOrDefault(c => c.Id == cityLocationForm.CityId);
                if (City != null)
                {
                    City.Longitude = cityLocationForm.Longitude;
                    City.Latitude = cityLocationForm.Latitude;
                    _context.Citys.Update(City);
                    await _context.SaveChangesAsync();
                    return true;
                }
                throw new ReservationGlobalException(CityServiceErrors.EditCityNotExistError);
            }
            catch (Exception ex)
            {

                throw new ReservationGlobalException(CityServiceErrors.CityLocationError, ex);
            }
        }

  

        public async Task<bool> SetCityImagesAsync(User user, CityImagesFormModel cityLocationForm)
        {
            try
            {
                City City = null;
                City = _context.Citys.FirstOrDefault(c => c.Id == cityLocationForm.CityId);
                if (City != null)
                {
                    if (cityLocationForm.UploadFiles != null && cityLocationForm.UploadFiles.Count > 0)
                    {
                        var notFoundFiles = 0;
                        var filesAddress = new List<FileAddress>();
                        cityLocationForm.UploadFiles.ForEach(uf =>
                        {
                            var fileAddress = _context.FileAddresses.Find(uf);
                            if (fileAddress == null)
                            {
                                notFoundFiles++;
                            }
                            filesAddress.Add(fileAddress);
                        });
                        if (notFoundFiles > 0)
                        {
                            throw new ReservationGlobalException(CityServiceErrors.NotExistFileAddresstError);
                        }

                        filesAddress.ForEach(async fa =>
                        {
                            var cityAttachment = new CityAttachment()
                            {
                                CityId = City.Id,
                                FilePath = fa.FilePath,
                                FileSize = fa.FileSize,
                                FileType = fa.FileType,
                            };
                            await _context.CityAttachments.AddAsync(cityAttachment);
                        });
                        await _context.SaveChangesAsync();
                    }
                    return true;
                }
                throw new ReservationGlobalException(CityServiceErrors.EditCityNotExistError);


            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(CityServiceErrors.AddCityError, ex);
            }
        }

        public async Task<bool> DeleteAttachmentCityAsync(User user, List<long> deleteAttachmentCityIds)
        {
            try
            {

                foreach (var deleteAttachmentCityId in deleteAttachmentCityIds)
                {
                    
                    var attCity = await _context.CityAttachments.FirstOrDefaultAsync(p => p.Id == deleteAttachmentCityId);
                    if (attCity!=null)                   
                    _context.CityAttachments.Remove(attCity);
                }
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new ReservationGlobalException(CityServiceErrors.DeleteAttachmentCityError, ex);
            }
        }
    }
}
