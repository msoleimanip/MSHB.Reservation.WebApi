using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface IAccommodationService
    {
        Task<bool> AddAsync(AddAccommodationFormModel accommodationForm);
        Task<SearchAccommodationViewModel> GetAsync(SearchAccommodationFormModel searchAccommodationForm);
    }
}
