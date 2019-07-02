using MSHB.Reservation.Layers.L01_Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MSHB.Reservation.Layers.L04_ViewModels.InputForms
{
    public class ReservationRoomPaiedFormModel
    {
        [Required(ErrorMessage = "شناسه اقامتگاه وارد نشده است")]
        public long ReservationUserRoomId { get; set; }
        public long PriceAccommodation { get; set; }
        public bool? IsPaied { get; set; }
        public PaymentType PaymentType { get; set; }
        public string Description { get; set; }
    }
}
