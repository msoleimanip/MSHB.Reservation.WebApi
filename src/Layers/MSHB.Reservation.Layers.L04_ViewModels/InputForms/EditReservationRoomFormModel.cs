using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class EditReservationRoomFormModel:AddReservationRoomFormModel
    {
        [Required(ErrorMessage = "شناسه رزرو وارد نشده است")]
        public long AccommodationUserRoomId { get; set; }
        
    }
}
