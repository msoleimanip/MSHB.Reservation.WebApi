using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;

namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface IReservationUserRoomService
    {
        Task<long> AddReservationRoomAsync(User user, AddReservationRoomFormModel reservationForm);
        Task<bool> EditReservationRoomAsync(User user, EditReservationRoomFormModel reservationForm);
        Task<bool> DeleteReservationRoomAsync(User user, List<long> reservationFormIds);
        Task<SearchReservationRoomViewModel> GetUserReservationRoomForUserAsync(User user, ReservationRoomSearchFormModel reservationForm);
    }
}
