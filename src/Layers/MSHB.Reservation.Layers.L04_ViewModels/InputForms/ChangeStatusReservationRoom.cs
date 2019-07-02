using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class ChangeStatusReservationRoom
    {
        [Required(ErrorMessage = "شناسه رزرو وارد نشده است")]
        public long AccommodationUserRoomId { get; set; }
        [Required(ErrorMessage = "انتخاب وضعیت الزامیست")]
        public StatusReservationType Status { get; set; }
    }
}
