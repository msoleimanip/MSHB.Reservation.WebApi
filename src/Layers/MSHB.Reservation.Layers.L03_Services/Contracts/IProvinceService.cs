using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface IProvinceService
    {
        Task<List<ProvinceViewModel>> GetAsync();
        Task<ProvinceViewModel> GetAsync(long Id);

    }
}
