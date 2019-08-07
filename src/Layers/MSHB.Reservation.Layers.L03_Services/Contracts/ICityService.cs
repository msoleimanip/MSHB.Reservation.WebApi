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
        Task<List<CityViewModel>> GetAllAsync();
        Task<List<CityViewModel>> GetAsync(long ProvinceId);
        Task<CityViewModel> GetAsync(long ProvinceId, long Id);
    }
}
