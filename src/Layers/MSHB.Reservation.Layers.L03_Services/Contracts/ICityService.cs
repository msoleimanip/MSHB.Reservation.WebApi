using Microsoft.AspNetCore.Http;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.Tree;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface ICityService
    {
        Task<CityViewModel> GetAsync(User user,long Id);
        Task<List<JsTreeNode>> GetCityByUserAsync(User user);
        Task<List<JsTreeNode>> GetUserCityForUserAsync(User user, Guid userId);
        Task<long> AddCityAsync(User user, AddcityFormModel cityForm);
        Task<bool> EditCityAsync(User user, EditcityFormModel cityForm);
        Task<bool> DeleteCityAsync(User user, List<long> CityIds);
        Task<bool> DeactivateCityAsync(User user, DeactivateCityFormModel cityForm);
        Task<bool> SetCityLocationAsync(User user, CityLocationFormModel cityLocationForm);
        Task<Guid> UploadFileAsync(User user, IFormFile file);
        Task<bool> SetCityImagesAsync(User user, CityImagesFormModel cityLocationForm);
    }
}
