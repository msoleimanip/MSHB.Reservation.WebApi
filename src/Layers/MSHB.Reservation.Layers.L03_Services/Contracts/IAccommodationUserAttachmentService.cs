using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MSHB.Reservation.Layers.L01_Entities.Models;
using MSHB.Reservation.Layers.L04_ViewModels.InputForms;
using MSHB.Reservation.Layers.L04_ViewModels.ViewModels;
namespace MSHB.Reservation.Layers.L03_Services.Contracts
{
    public interface IAccommodationUserAttachmentService
    {
        Task<SearchReservationUserAttachmentViewModel> GetReservationUserAttachmentAsync(User user, ReservationUserAttachmentSearchFormModel reservationForm);
        Task<bool> DeleteReservationUserAttachmentAsync(User user, List<long> reservationFormIds);
        Task<bool> EditReservationUserAttachmentRoomAsync(User user, EditReservationUserAttachmentFormModel reservationForm);
        Task<long> AddReservationUserAttachmentRoomAsync(User user, AddReservationUserAttachmentFormModel reservationForm);
    }
}