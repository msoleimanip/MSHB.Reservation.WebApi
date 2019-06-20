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
        Task<long> AddAccommodationRoom(User user, AddAccommodationRoomFormModel accommodationForm);
        Task<bool> EditAccommodationRoom(User user, EditAccommodationRoomFormModel accommodationForm);
        Task<bool> DeleteAccommodationRoom(User user, List<long> accommodationFormIds);   
        Task<bool> DeactivateAccommodationRoomAsync(User user, DeactivateAccommodationRoomFormModel accommodationForm);
        Task <SearchAccommodationRoomViewModel> GetUserAccommodationRoomForUserAsync(User user, AccommodationRoomSearchFormModel accommodationForm);
    }
}
